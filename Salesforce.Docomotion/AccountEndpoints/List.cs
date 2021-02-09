using System;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salesforce.Docomotion.Domain;
using Salesforce.Docomotion.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Salesforce.Docomotion.AccountEndpoints
{
    public class List : BaseAsyncEndpoint
    {
        private ILogger<List> _logger;
        private IAccountService _accountService;

        public List(
            ILogger<List> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Retrieves Salesforce's Accounts.
        /// </summary>
        /// <returns>Retrieved list of <see cref="Account"/>.</returns>
        [HttpGet("/accounts")]
        [SwaggerOperation(
            Summary = "List all Accounts",
            Description = "List all Accounts",
            OperationId = "Accounts.List",
            Tags = new[] { "AccountsEndpoint" })
        ]
        public async Task<IActionResult> GetAccounts()
        {
            try
            {
                var accounts = await _accountService.GetListAsync();
                _logger.LogDebug("Successfully retrieved Accounts.");

                return Ok(accounts);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to retrieve Accounts. Error: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
