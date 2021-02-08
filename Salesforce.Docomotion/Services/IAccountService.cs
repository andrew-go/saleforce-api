using System.Collections.Generic;
using System.Threading.Tasks;
using Salesforce.Docomotion.Domain;
using Salesforce.Docomotion.Utils.FileGenerator;

namespace Salesforce.Docomotion.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetListAsync();

        Task<Account> GetByIdAsync(string id);

        Task UpdateByIdAsync(string id, Account account);

        Task<File> GetAccountsFile(Format format);
    }
}
