namespace Orbital.Reflection
{
    public sealed class PropertyDescription : BaseDescription
    {
        #region Properties

        public TypeDescription TypeDescription { get; internal set; }

        public bool IsField { get; private set; }

        #endregion

        #region Constructors

        internal PropertyDescription(string name, bool isField)
            : base(name)
        {
            IsField = isField;
        }

        #endregion
    }
}