using System;

namespace Orbital.Serializer
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class SerializerAttribute : Attribute
    {
        #region Properties

        public string[] Extensions { get; set; }

        #endregion
    }
}