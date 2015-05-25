using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class PrimitiveValueSource : ValueSource
    {
        #region Constructors

        internal PrimitiveValueSource(string name, TypeDescription type, IValueSource source) : base(name, type, source)
        {
        }

        #endregion
    }
}