namespace Orbital.Data
{
    internal sealed class CollectionDescription : TypeDescription
    {
        #region Fields

        private readonly TypeDescription[] _itemTypes;

        #endregion

        #region Properties

        public TypeDescription[] ItemTypes
        {
            get { return _itemTypes; }
        }

        public bool IsKeyed
        {
            get { return Kind == TypeKind.Dictionary; }
        }

        #endregion

        #region Constructors

        internal CollectionDescription(string name, TypeKind kind) : base(name, kind)
        {
        }

        #endregion
    }
}
