namespace Orbital.Reflection
{
    internal class ObjectDescription
    {
        #region Fields

        private object _parent;
        private object _data;

        #endregion

        #region Constructors

        internal ObjectDescription(object data, object parent)
        {
            _parent = parent;
            _data = data;
        }

        #endregion
    }
}