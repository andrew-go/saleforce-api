using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Salesforce.Docomotion.Api;
using Salesforce.Docomotion.Domain;
using Salesforce.Docomotion.Utils.FileGenerator;

namespace Salesforce.Docomotion.Services
{
    public class AccountService : IAccountService
    {
        private readonly ISalesForceApi _salesForceApi;
        private readonly IFileGeneratorFactory _fileGeneratorFactory;

        public AccountService(ISalesForceApi salesForceApi, IFileGeneratorFactory fileGeneratorFactory)
        {
            _salesForceApi = salesForceApi;
            _fileGeneratorFactory = fileGeneratorFactory;
        }

        public async Task<List<Account>> GetListAsync()
        {
            var fields = typeof(Account).GetProperties().Select(p => p.Name);
            var query = $"SELECT+{string.Join(',', fields)}+FROM+Account";
            var queryResult = await _salesForceApi.GetByQueryAsync<Account>(query);
            return queryResult.Records;
        }

        public async Task<Account> GetByIdAsync(string id)
        {
            return await _salesForceApi.GetById<Account>(nameof(Account), id);
        }

        public async Task UpdateByIdAsync(string id, Account account)
        {
            await _salesForceApi.UpdateById(nameof(Account), id, account);
        }

        public async Task<File> GetAccountsFile(Format format)
        {
            var accounts = await GetListAsync();
            var fileGenerator = _fileGeneratorFactory.GetFileGenerator(format);
            var file = fileGenerator.GenerateFromObjects(accounts);
            return file;
        }
    }
}
