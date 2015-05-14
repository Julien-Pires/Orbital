namespace Orbital.Data
{
    internal sealed class PropertyDescription : BaseDescription
    {
        #region Fields

        private readonly TypeDescription _typeDescription;

        #endregion

        #region Properties

        public TypeDescription TypeDescription
        {
            get { return _typeDescription; }
        }

        #endregion

        #region Constructors

        internal PropertyDescription(string name, TypeDescription typeDescriptionDescription)
            : base(name)
        {
            _typeDescription = typeDescriptionDescription;
        }

        #endregion
    }
}