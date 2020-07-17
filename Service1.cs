using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace BareBoneMembershipApi
{
    public class Service1 : IService
    {
        private readonly HttpClient _httpClient;    
        private const string BaseUrl = "https://XYZ/WHICSServices/api/assessing/memberships/";
        
        public Service1(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
        }
        public async Task<Client> FetchClient( int clientId)  
        {
            HttpResponseMessage httpResponseMessage = null;
            try
            {
                httpResponseMessage = await _httpClient.GetAsync($"fetchClientByNumber?clientNumber={clientId}");
                httpResponseMessage.EnsureSuccessStatusCode();

                var result = await httpResponseMessage.Content.ReadAsAsync<ApiResponse<Client>>();

                return result.Data;
            }
            finally
            {
                //Tried without this as well.
                httpResponseMessage?.Dispose();
            }
        }
    }
}