namespace Orbital.Reflection
{
    public abstract class BaseDescription
    {
        #region Fields

        private readonly string _name;

        #endregion

        #region Properties

        public string Name
        {
            get { return _name; }
        }

        #endregion

        #region Constructors

        protected BaseDescription(string name)
        {
            _name = name;
        }

        #endregion
    }
}