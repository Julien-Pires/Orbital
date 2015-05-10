using System;

namespace Orbital.Source
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class DataSourceAttribute : Attribute
    {
        #region Properties

        public string[] Extensions { get; set; }

        #endregion
    }
}