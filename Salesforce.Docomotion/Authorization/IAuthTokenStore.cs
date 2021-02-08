using System.Threading.Tasks;

namespace Salesforce.Docomotion.Authorization
{
    public interface IAuthTokenStore
    {
        Task<string> GetTokenAsync();
    }
}
