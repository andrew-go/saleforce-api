namespace Salesforce.Docomotion.Domain
{
    public class Account
    {
        public string? Id { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }

        public BillingAddress? BillingAddress { get; set; }

        public string? Phone { get; set; }

        public string? Fax { get; set; }

        public string? AccountNumber { get; set; }

        public string? Website { get; set; }

        public string? PhotoUrl { get; set; }

        public string? Sic { get; set; }

        public string? Industry { get; set; }

        public long? AnnualRevenue { get; set; }

        public int? NumberOfEmployees { get; set; }

        public string? Ownership { get; set; }

        public string? TickerSymbol { get; set; }
    }
}
