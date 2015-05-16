namespace Orbital.Data
{
    internal sealed class PropertyDescription : BaseDescription
    {
        #region Properties

        public TypeDescription TypeDescription { get; internal set; }

        #endregion

        #region Constructors

        internal PropertyDescription(string name)
            : base(name)
        {
        }

        #endregion
    }
}