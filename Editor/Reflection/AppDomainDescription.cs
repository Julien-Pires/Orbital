using System;
using System.Linq;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

using Orbital.Extension;

namespace Orbital.Reflection
{
    internal sealed class AppDomainDescription : BaseDescription
    {
        #region Fields

        private const BindingFlags FieldsFlag = BindingFlags.Instance | BindingFlags.Public;

        private readonly Dictionary<string, AssemblyDescription> _assemblies =
            new Dictionary<string, AssemblyDescription>();

        #endregion

        #region Constructors

        internal AppDomainDescription(string domainName) : base(domainName)
        {
        }

        #endregion

        #region Assembly Methods

        private AssemblyDescription EnsureAssembly(string name)
        {
            AssemblyDescription assembly;
            if (!_assemblies.TryGetValue(name, out assembly))
            {
                assembly = new AssemblyDescription(name);
                _assemblies.Add(name, assembly);
            }

            return assembly;
        }

        #endregion

        #region Type Methods

        public TypeDescription GetType(string assemblyName, string fullname)
        {
            string namespaceName, name;
            DescriptionHelper.ExtractNames(fullname, out namespaceName, out name);

            return EnsureAssembly(assemblyName).GetType(namespaceName, name);
        }

        public TypeDescription RegisterType(Type type)
        {
            return RegisterTypeFromClrType(type);
        }

        #endregion

        #region Reflection Methods

        private TypeDescription RegisterTypeFromClrType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            if (type.IsPrimitive)
                return RegisterPrimitive(type);

            if (type.IsEnum)
                return RegisterEnum(type);

            if (type.IsClass || type.IsValueType)
            {
                if (type.IsArray || type.ImplementsInterfaces(typeof(IList), typeof(IDictionary)))
                    return RegisterCollection(type);

                return RegisterObject(type);
            }

            throw new ArgumentException(string.Format("Failed to create type from {0}", type));
        }

        private TypeDescription RegisterObject(Type type)
        {
            AssemblyDescription assembly = EnsureAssembly(type.Assembly.GetName().Name);
            ObjectDescription objDescription = new ObjectDescription(type.Name,
                (type.IsClass ? TypeKind.Class : TypeKind.Struct), type);
            assembly.RegisterType(type.Namespace, objDescription);

            ExtractObjectProperties(objDescription);
            ExtractObjectKeys(objDescription);

            return objDescription;
        }

        private TypeDescription RegisterCollection(Type type)
        {
            TypeKind kind;
            Type[] elementTypes;
            string name = type.Name;
            if (!type.IsArray)
            {
                if (type.GetInterfaces().Any(c => c == typeof (IList)))
                    kind = TypeKind.List;
                else if (type.GetInterfaces().Any(c => c == typeof (IDictionary)))
                    kind = TypeKind.Dictionary;
                else
                    throw new ArgumentException(string.Format("{0} is not a supported collection type", type));

                elementTypes = type.GetGenericArguments();
                if (elementTypes.Length == 0)
                    throw new InvalidOperationException(string.Format("Only generics collection are supported : {0}", type));

                name = type.GetGenericsName();
            }
            else
            {
                kind = TypeKind.Array;
                elementTypes = new []{ type.GetElementType() };
            }

            CollectionDescription collectionDescription = new CollectionDescription(name, kind, type);
            AssemblyDescription assembly = EnsureAssembly(type.Assembly.GetName().Name);
            assembly.RegisterType(type.Namespace, collectionDescription);

            for(int i = 0; i < elementTypes.Length; i++)
                collectionDescription.AddType(GetOrCreateType(elementTypes[i]));

            return collectionDescription;
        }

        private TypeDescription RegisterPrimitive(Type type)
        {
            AssemblyDescription assembly = EnsureAssembly(type.Assembly.GetName().Name);
            TypeDescription typeDescription = TypeDescription.GetPrimitiveType(type);
            assembly.RegisterType(type.Namespace, typeDescription);

            return typeDescription;
        }

        private TypeDescription RegisterEnum(Type type)
        {
            AssemblyDescription assembly = EnsureAssembly(type.Assembly.GetName().Name);
            TypeDescription typeDescription = new EnumDescription(type.Name, Enum.GetNames(type), type);
            assembly.RegisterType(type.Namespace, typeDescription);

            return typeDescription;
        }

        private TypeDescription GetOrCreateType(Type type)
        {
            string assemblyName = type.Assembly.GetName().Name;

            return GetType(assemblyName, type.FullName) ?? RegisterTypeFromClrType(type);
        }

        private void ExtractObjectProperties(ObjectDescription objDescription)
        {
            PropertyInfo[] properties = objDescription.CLRType.GetProperties(FieldsFlag);
            for (int i = 0; i < properties.Length; i++)
            {
                if(!properties[i].CanRead || !properties[i].CanWrite)
                    continue;

                if(properties[i].GetIndexParameters().Length > 0)
                    continue;

                TypeDescription typeDescription = GetOrCreateType(properties[i].PropertyType);
                PropertyDescription propertyDescription = new PropertyDescription(properties[i].Name, false)
                {
                    TypeDescription = typeDescription
                };

                objDescription[propertyDescription.Name] = propertyDescription;
            }

            FieldInfo[] fields = objDescription.CLRType.GetFields(FieldsFlag);
            for (int i = 0; i < fields.Length; i++)
            {
                if (fields[i].IsInitOnly)
                    continue;

                TypeDescription typeDescription = GetOrCreateType(fields[i].FieldType);
                PropertyDescription propertyDescription = new PropertyDescription(fields[i].Name, true)
                {
                    TypeDescription = typeDescription
                };

                objDescription[propertyDescription.Name] = propertyDescription;
            }
        }

        private static void ExtractObjectKeys(ObjectDescription objDescription)
        {
            Type objType = objDescription.CLRType;
            var members = objType.GetProperties().Cast<MemberInfo>().Union(objType.GetFields());
            MemberInfo primaryKey = members.Where(c => c.GetCustomAttributes(typeof(PrimaryKey), true).Length > 0).FirstOrDefault();
            if (primaryKey != null)
                objDescription[primaryKey.Name].IsPrimaryKey = true;
        }

        #endregion
    }
}