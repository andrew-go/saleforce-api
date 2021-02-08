using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salesforce.Docomotion.Domain;
using Salesforce.Docomotion.Services;
using Salesforce.Docomotion.Utils.FileGenerator;

namespace Salesforce.Docomotion.Controllers
{
    [ApiController]
    [Route("/accounts")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAccountService _accountService;

        public AccountController(
            ILogger<AccountController> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Retrieves Salesforce's Accounts.
        /// </summary>
        /// <returns>Retrieved list of <see cref="Account"/>.</returns>
        [HttpGet]
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

        /// <summary>
        /// Retrieves Salesforce's Account.
        /// </summary>
        /// <param name="id">An id of Account.</param>
        /// <returns>Retrieved <see cref="Account"/>.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccount([Required] string id)
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

        /// <summary>
        /// Updates Salesforce's Account.
        /// </summary>
        /// <param name="id">An id of Account.</param>
        /// <param name="account">A <see cref="Account"/> object for updating Salesforce's Account</param>
        /// <returns>No content.</returns>
        [HttpPut("{id}")]
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

        /// <summary>
        /// Downloads Accounts file in particular <see cref="Format"/>.
        /// </summary>
        /// <param name="format">A <see cref="Format"/> in which file should be generated.</param>
        /// <returns>File with particular <see cref="Format"/>.</returns>
        [HttpGet("download/{format}")]
        public async Task<IActionResult> DownloadAccountsFile([Required] Format format)
        {
            try
            {
                var file = await _accountService.GetAccountsFile(format);
                _logger.LogDebug($"Successfully generated {file.Name} file.");

                return File(file.Data, file.ContentType, file.Name);
            }
            catch (Exception e)
            {
                _logger.LogError($"Failed to retrieve Account. Error: {e.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
