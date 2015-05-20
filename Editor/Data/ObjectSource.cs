using Orbital.Reflection;

namespace Orbital.Data
{
    public sealed class ObjectSource : PropertySource
    {
        #region Constructors

        internal ObjectSource(object parent, PropertyDescription property) : base(parent, property)
        {
            
        }

        #endregion
    }
}