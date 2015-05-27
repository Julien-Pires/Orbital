using System;
using System.Collections.Generic;

namespace Orbital.Reflection
{
    internal sealed class AppDomainManager : IAppDomainManager
    {
        #region Fields

        private readonly Dictionary<string, AppDomainDescription> _appDomains = new Dictionary<string, AppDomainDescription>();

        #endregion

        #region Methods

        public AppDomainDescription CreateDomain(string domainName)
        {
            if (string.IsNullOrEmpty(domainName))
                throw new ArgumentNullException("domainName");

            if (_appDomains.ContainsKey(domainName))
                throw new ArgumentException(string.Format("Domain {0} already exist", "domainName"));

            AppDomainDescription domain = new AppDomainDescription(domainName);
            _appDomains[domainName] = domain;

            return domain;
        }

        public AppDomainDescription GetDomain(string domainName)
        {
            if(string.IsNullOrEmpty(domainName))
                throw new ArgumentNullException("domainName");

            AppDomainDescription domain;
            if (!_appDomains.TryGetValue(domainName, out domain))
                return null;

            return domain;
        }

        #endregion
    }
}