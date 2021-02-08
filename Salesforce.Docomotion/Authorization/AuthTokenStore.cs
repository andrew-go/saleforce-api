using System.Threading.Tasks;
using Refit;
using Salesforce.Docomotion.Api;
using Salesforce.Docomotion.Configuration;

namespace Salesforce.Docomotion.Authorization
{
    public class AuthTokenStore : IAuthTokenStore
    {
        private readonly SalesForceAuthConfig _salesForceAuthConfig;
        private static readonly string HostUrlValue = "https://login.salesforce.com";
        public AuthTokenStore(SalesForceAuthConfig salesForceAuthConfig)
        {
            _salesForceAuthConfig = salesForceAuthConfig;
        }

        public async Task<string> GetTokenAsync()
        {
            var gitHubApi = RestService.For<ISalesForceAuthApi>(HostUrlValue);
            var response = await gitHubApi.Oauth(_salesForceAuthConfig);
            var token = response["access_token"];
            return token;
        }
    }
}
