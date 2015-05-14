namespace Orbital
{
    public sealed class Schema
    {
        #region Properties

        public string Namespace { get; set; }

        public ObjectSchema[] Objects { get; set; }

        #endregion
    }
}