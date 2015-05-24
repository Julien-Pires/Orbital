using Orbital.Reflection;

namespace Orbital.Data
{
    internal static class DataSourceHelper
    {
        #region Factory Methods

        public static IValueSource CreateSource(string name, TypeDescription type, IValueSource source)
        {
            IValueSource result;
            switch(type.Kind)
            {
                case TypeKind.Class:
                case TypeKind.Struct:
                    result = new ObjectSource(name, (ObjectDescription)type, source);
                    break;

                case TypeKind.Array:
                case TypeKind.List:
                case TypeKind.Dictionary:
                    result = new CollectionSource(name, type, source);
                    break;

                case TypeKind.Enum:
                    result = new EnumSource(name, type, source);
                    break;
                
                default:
                    result = new ValueSource(name, type, source);
                    break;
            }

            return result;
        }

        #endregion
    }
}