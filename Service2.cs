using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BareBoneMembershipApi
{
    public class Service2 : IService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public Service2(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<Client> FetchClient(int clientId) 
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                var client = _httpClientFactory.CreateClient(_configuration["Api:HttpClientName"]);
                httpResponseMessage = await client.GetAsync($"fetchClientByNumber?clientNumber={clientId}");
                httpResponseMessage.EnsureSuccessStatusCode();

                var result = await httpResponseMessage.Content.ReadAsAsync<ApiResponse<Client>>();

                return result.Data;
            }
            finally
            {
                httpResponseMessage?.Dispose();
            }
        }
    }
}