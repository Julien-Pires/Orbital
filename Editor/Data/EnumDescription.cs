using System.Linq;
using System.Collections.Generic;

namespace Orbital.Data
{
    internal sealed class EnumDescription : TypeDescription
    {
        #region Fields

        private readonly string[] _values;

        #endregion

        #region Properties

        public string[] Values
        {
            get { return _values; }
        }

        #endregion

        #region Constructors

        internal EnumDescription(string name, IEnumerable<string> values) : base(name, TypeKind.Enum)
        {
            _values = values.ToArray();
        }

        #endregion
    }
}