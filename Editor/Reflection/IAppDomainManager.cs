namespace Orbital.Reflection
{
    internal interface IAppDomainManager
    {
        #region Methods

        AppDomainDescription CreateDomain(string domainName);

        AppDomainDescription GetDomain(string domainName);

        #endregion
    }
}