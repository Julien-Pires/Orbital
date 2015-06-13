using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class EnumSource : ValueSource<EnumDescription>
    {
        #region Constructors

        internal EnumSource(string name, EnumDescription type, IValueSource source) : base(name, type, source)
        {
        }

        #endregion
    }
}