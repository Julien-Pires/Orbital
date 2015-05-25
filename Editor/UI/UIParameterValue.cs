namespace Orbital.UI
{
    public sealed partial class UIParameters
    {
        private sealed class UIParameterValue<T>
        {
            #region Properties

            public T Value { get; set; }

            #endregion
        }
    }
}