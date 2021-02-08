using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using Salesforce.Docomotion.Authorization;
using Salesforce.Docomotion.Configuration;

namespace Salesforce.Docomotion.Api
{
    public interface ISalesForceAuthApi
    {
        [Post("/services/oauth2/token")]
        Task<Dictionary<string, string>> Oauth([Query] SalesForceAuthConfig authConfig);
    }
}
