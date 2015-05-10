namespace Orbital.Data
{
    internal sealed class ObjectDescription : BaseDescription
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

        internal ObjectDescription(string name, TypeKind kind) : base(name)
        {
            _isClass = (kind == TypeKind.Class);
        }

        #endregion
    }
}