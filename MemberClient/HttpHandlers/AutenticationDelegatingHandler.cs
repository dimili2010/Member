using IdentityModel.Client;

namespace MemberClient.HttpHandlers
{
    public class AutenticationDelegatingHandler(IHttpClientFactory httpClientFactory, ClientCredentialsTokenRequest clientCredentialsTokenRequest) : DelegatingHandler
    {
        private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;
        private readonly ClientCredentialsTokenRequest _tokenRequest = clientCredentialsTokenRequest;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var httpClient = _httpClientFactory.CreateClient("IdentityServer");
            var tokenResponse = await httpClient.RequestClientCredentialsTokenAsync(_tokenRequest, cancellationToken: cancellationToken);

            if (tokenResponse.IsError)
            {
                throw new HttpRequestException("Error token generation");
            }

            request.SetBearerToken(tokenResponse.AccessToken);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
