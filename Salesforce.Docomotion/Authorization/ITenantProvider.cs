namespace Salesforce.Docomotion.Authorization
{
    public interface ITenantProvider
    {
        string GetTenantId();
    }
}
