namespace Orbital
{
    public sealed class PropertySchema
    {
        #region Properties

        public string Name { get; set; }

        public TypeKind Kind { get; set; }

        public string[] Types { get; set; }

        #endregion
    }
}