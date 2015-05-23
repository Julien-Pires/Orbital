using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class EnumSource : ValueSource
    {
        #region Constructors

        internal EnumSource(string name, TypeDescription type, IValueSource source) : base(name, type, source)
        {
        }

        #endregion
    }
}