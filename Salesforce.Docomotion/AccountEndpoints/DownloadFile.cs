using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Salesforce.Docomotion.Services;
using Salesforce.Docomotion.Utils.FileGenerator;
using Swashbuckle.AspNetCore.Annotations;

namespace Salesforce.Docomotion.AccountEndpoints
{
    public class DownloadFile : BaseAsyncEndpoint
    {
        private ILogger<DownloadFile> _logger;
        private IAccountService _accountService;

        public DownloadFile(
            ILogger<DownloadFile> logger,
            IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        /// <summary>
        /// Downloads Accounts file in particular <see cref="Format"/>.
        /// </summary>
        /// <param name="format">A <see cref="Format"/> in which file should be generated.</param>
        /// <returns>File with particular <see cref="Format"/>.</returns>
        [HttpGet("/accounts/download/{format}")]
        [SwaggerOperation(
            Summary = "Download Accounts file with particular format",
            Description = "Download Accounts file with particular format",
            OperationId = "Accounts.DownloadFile",
            Tags = new[] { "AccountsEndpoint" })
        ]
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
