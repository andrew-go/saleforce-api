using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salesforce.Docomotion.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace Salesforce.Docomotion.AccountEndpoints
{
    public class Get : BaseAsyncEndpoint
    {
        private ILogger<Get> _logger;
        private IAccountService _accountService;

        public Get(
            ILogger<Get> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        [HttpGet("/accounts/{id}")]
        [SwaggerOperation(
            Summary = "Get Account by id",
            Description = "Get Account by id",
            OperationId = "Accounts.Get",
            Tags = new[] { "AccountsEndpoint" })
        ]
        public async Task<ActionResult> HandleAsync(
            [Required] string id)
        {
            try
            {
                var account = await _accountService.GetByIdAsync(id);
                _logger.LogDebug("Successfully retrieved Account.");

                return Ok(account);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to retrieve Account. Error: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
