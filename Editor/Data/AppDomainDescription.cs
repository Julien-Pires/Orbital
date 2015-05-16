using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

using UnityEngine;

namespace Orbital.Data
{
    internal sealed class AppDomainDescription : BaseDescription
    {
        #region Fields

        private readonly Dictionary<string, AssemblyDescription> _assemblies = new Dictionary<string, AssemblyDescription>();

        #endregion

        #region Constructors

        internal AppDomainDescription() : base(Application.productName)
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

        internal AssemblyDescription CreateFromSchema(Schema schema)
        {
            if (schema == null)
                throw new ArgumentNullException("schema");

            ObjectSchema[] objSchemas = schema.Objects;
            AssemblyDescription assembly = new AssemblyDescription(schema.Namespace);
            for (int i = 0; i < objSchemas.Length; i++)
            {
                ObjectSchema currObjectSchema = objSchemas[i];
                TypeDescription typeDescription = CreateTypeFromSchema(currObjectSchema);
                assembly.RegisterType(currObjectSchema.Namespace, typeDescription);
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

        #region Schema Methods

        private TypeDescription CreateType(string name, TypeKind kind, object parameters = null)
        {
            TypeDescription type = TypeDescription.GetPrimitiveType(kind);
            if (type != null)
                return type;

            switch (kind)
            {
                case TypeKind.Enum:
                    type = new EnumDescription(name, (IEnumerable<string>)parameters);
                    break;

                case TypeKind.Class:
                case TypeKind.Struct:
                    type = new ObjectDescription(name, kind);
                    break;

                case TypeKind.Array:
                case TypeKind.List:
                case TypeKind.Dictionary:
                    type = new CollectionDescription(name, kind);
                    break;
            }

            if (type == null)
                throw new InvalidOperationException(string.Format("{0} is not a supported type", kind));

            return type;
        }

        private TypeDescription CreateTypeFromSchema(ObjectSchema objectSchema)
        {
            if (objectSchema == null)
                throw new ArgumentNullException("objectSchema");

            return CreateType(objectSchema.Name, objectSchema.Kind, objectSchema.Values);
        }

        #endregion

        #region Reflection Methods

        private TypeDescription RegisterTypeFromClrType(Type type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            TypeDescription typeDescription = null;
            if (type.IsPrimitive)
            {
                typeDescription = TypeDescription.GetPrimitiveType(type);
            }
            else if (type.IsEnum)
            {
                typeDescription = new EnumDescription(type.Name, Enum.GetValues(type).Cast<string>());
            }
            else if (type.IsClass || type.IsValueType)
            {
                typeDescription = CreateObjectType(type);
            }

            if(typeDescription == null)
                throw new InvalidOperationException(string.Format("Failed to create type from {0}", type));

            AssemblyDescription assembly = EnsureAssembly(type.Assembly.GetName().Name);
            assembly.RegisterType(type.Namespace, typeDescription);

            return typeDescription;
        }

        private TypeDescription GetOrCreateType(Type type)
        {
            string assemblyName = type.Assembly.GetName().Name;

            return GetType(assemblyName, type.FullName) ?? RegisterTypeFromClrType(type);
        }

        private ObjectDescription CreateObjectType(Type type)
        {
            ObjectDescription objDescription = new ObjectDescription(type.Name, (type.IsClass ? TypeKind.Class : TypeKind.Struct));
            PropertyInfo[] properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public
                                                           | BindingFlags.GetProperty | BindingFlags.SetProperty);
            for (int i = 0; i < properties.Length; i++)
            {
                Type propertyType = properties[i].PropertyType;
                TypeDescription typeDescription = GetOrCreateType(propertyType);
                PropertyDescription propertyDescription = new PropertyDescription(properties[i].Name)
                {
                    TypeDescription = typeDescription
                };

                objDescription[propertyDescription.Name] = propertyDescription;
            }

            return objDescription;
        }

        #endregion
    }
}