using System.Collections.Generic;

namespace Orbital
{
    public sealed class DataIndex
    {
        #region Properties

        public List<DataSource> Files { get; set; }

        public string ForeignKeys { get; set; }

        #endregion

        #region Constructors

        public DataIndex()
        {
            Files = new List<DataSource>();
        }

        #endregion
    }
}
