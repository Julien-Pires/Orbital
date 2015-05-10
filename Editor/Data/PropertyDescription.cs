namespace Orbital.Data
{
    internal sealed class PropertyDescription : BaseDescription
    {
        #region Fields

        private readonly TypeDescription _type;

        #endregion

        #region Properties

        public TypeDescription Type
        {
            get { return _type; }
        }

        #endregion

        #region Constructors

        internal PropertyDescription(string name, TypeKind kind)
            : base(name)
        {
            _type = TypeDescription.GetType(kind);
        }

        #endregion
    }
}