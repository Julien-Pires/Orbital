namespace Orbital.Source
{
    internal interface IDataSourceManager
    {
        #region Methods

        void AddDataSource(string extension, IDataSource source);

        IDataSource GetDataSource(string extension);

        #endregion
    }
}