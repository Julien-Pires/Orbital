namespace Orbital
{
    public sealed class Schema
    {
        #region Properties

        public string Namespace { get; set; }

        public TypeSchema[] Types { get; set; }

        #endregion
    }
}