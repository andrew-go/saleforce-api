using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Salesforce.Docomotion.Authorization
{
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ITenantProvider _tenantProvider;
        private readonly IAuthTokenStore _authTokenStore;
        private string? _authToken;

        public AuthHeaderHandler(
            ITenantProvider tenantProvider,
            IAuthTokenStore authTokenStore)
        {
            _tenantProvider = tenantProvider ?? throw new ArgumentNullException(nameof(tenantProvider));
            _authTokenStore = authTokenStore ?? throw new ArgumentNullException(nameof(authTokenStore));
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // TODO: also would be great to add expiration logic
            _authToken ??= await _authTokenStore.GetTokenAsync();

            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _authToken);
            request.Headers.Add("X-Tenant-Id", _tenantProvider.GetTenantId());

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
    }
}
