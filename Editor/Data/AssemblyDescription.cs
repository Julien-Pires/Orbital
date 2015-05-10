using System.Collections.Generic;

using Orbital;
using Orbital.Data;

namespace Assets.Orbital.Editor.Data
{
    internal sealed class AssemblyDescription : BaseDescription
    {
        #region Fields

        private readonly Dictionary<string, ObjectDescription> _objectTypes = new Dictionary<string, ObjectDescription>();

        #endregion

        #region Constructors

        internal AssemblyDescription(string name) : base(name)
        {
        }

        #endregion

        #region Type Management Methods

        public void RegisterType(string name, TypeKind kind)
        {
        }

        #endregion
    }
}