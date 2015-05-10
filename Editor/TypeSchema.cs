namespace Orbital
{
    public sealed class TypeSchema
    {
        #region Properties

        public string Name { get; set; }

        public TypeKind Kind { get; set; }

        public PropertySchema[] Properties { get; set; }

        public string[] Values { get; set; }

        #endregion
    }
}