using System;

namespace Salesforce.Docomotion.Authorization
{
    class TenantProvider : ITenantProvider
    {
        public string GetTenantId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
