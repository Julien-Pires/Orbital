using Orbital.Reflection;

namespace Orbital.Data
{
    internal sealed class PrimitiveValueSource : ValueSource<PrimitiveDescription>
    {
        #region Constructors

        internal PrimitiveValueSource(string name, PrimitiveDescription type, IValueSource source) : base(name, type, source)
        {
        }

        #endregion
    }
}