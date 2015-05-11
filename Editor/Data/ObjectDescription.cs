namespace Orbital.Data
{
    internal sealed class ObjectDescription : TypeDescription
    {
        #region Fields

        private readonly bool _isClass;

        #endregion

        #region Properties

        public bool IsClass
        {
            get { return _isClass; }
        }

        #endregion

        #region Constructors

        internal ObjectDescription(string name, TypeKind kind) : base(name, kind)
        {
            _isClass = (kind == TypeKind.Class);
        }

        #endregion
    }
}