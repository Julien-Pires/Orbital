using System;

namespace Orbital
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class PrimaryKey : Attribute
    {
    }
}