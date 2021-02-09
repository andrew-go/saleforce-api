using System;
using System.ComponentModel.DataAnnotations;
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
    public class Update : BaseAsyncEndpoint
    {
        private ILogger<Update> _logger;
        private IAccountService _accountService;

        public Update(
            ILogger<Update> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Updates Salesforce's Account.
        /// </summary>
        /// <param name="id">An id of Account.</param>
        /// <param name="account">A <see cref="Account"/> object for updating Salesforce's Account</param>
        /// <returns>No content.</returns>
        [HttpPut("/accounts/{id}")]
        [SwaggerOperation(
            Summary = "Update Account",
            Description = "Update Account",
            OperationId = "Accounts.Update",
            Tags = new[] { "AccountsEndpoint" })
        ]
        public async Task<IActionResult> UpdateAccount(
            [Required] string id,
            [FromBody] Account account)
        {
            try
            {
                await _accountService.UpdateByIdAsync(id, account);
                _logger.LogDebug("Successfully updated Account.");

                return NoContent();
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to update Account. Error: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
