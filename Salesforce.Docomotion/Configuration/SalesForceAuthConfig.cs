using System.ComponentModel.DataAnnotations;
using Refit;

namespace Salesforce.Docomotion.Configuration
{
    public class SalesForceAuthConfig
    {
        [Required]
        [AliasAs("grant_type")]
        public string? GrantType { get; set; }

        [Required]
        [AliasAs("client_id")]
        public string? ClientId { get; set; }

        [Required]
        [AliasAs("client_secret")]
        public string? ClientSecret { get; set; }

        [Required]
        [AliasAs("username")]
        public string? Username { get; set; }

        [AliasAs("password")]
        public string? Password { get; set; }
    }
}
