using System;

namespace Orbital.Data
{
    internal static class DescriptionFactory
    {
        #region Type Factory Methods

        internal static AssemblyDescription CreateFromSchema(Schema schema)
        {
            if(schema == null)
                throw new ArgumentNullException("schema");

            ObjectSchema[] objSchemas = schema.Objects;
            AssemblyDescription assembly = new AssemblyDescription(schema.Namespace);
            for (int i = 0; i < objSchemas.Length; i++)
            {
                string namespaceName, name;
                ObjectSchema currObjectSchema = objSchemas[i];
                DescriptionHelper.ExtractNames(currObjectSchema.Name, out namespaceName, out name);

                TypeDescription typeDescription = CreateTypeFromSchema(name, currObjectSchema);
                assembly.RegisterType(namespaceName, typeDescription);
            }

            return assembly;
        }

        internal static TypeDescription CreateTypeFromSchema(string name, ObjectSchema objectSchema)
        {
            if(objectSchema == null)
                throw new ArgumentNullException("objectSchema");

            TypeDescription type = TypeDescription.GetPrimitiveType(objectSchema.Kind);
            if (type != null)
                return type;

            switch (objectSchema.Kind)
            {
                case TypeKind.Enum:
                    type = new EnumDescription(name, objectSchema.Values);
                    break;

                case TypeKind.Class:
                case TypeKind.Struct:
                    type = new ObjectDescription(name, objectSchema.Kind);
                    break;

                case TypeKind.Array:
                case TypeKind.List:
                case TypeKind.Dictionary:
                    type = new CollectionDescription(name, objectSchema.Kind);
                    break;
            }

            if(type == null)
                throw new InvalidOperationException(string.Format("{0} is not a supported type", objectSchema.Kind));

            return type;
        }

        internal static void CreatePropertiesFromSchema()
        {
        }

        #endregion
    }
}