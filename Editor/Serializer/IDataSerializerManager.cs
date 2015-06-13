namespace Orbital.Serializer
{
    internal interface IDataSerializerManager
    {
        #region Methods

        void AddSerializer(string extension, ISerializer source);

        ISerializer GetSerializer(string extension);

        #endregion
    }
}