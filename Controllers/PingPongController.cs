using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using BareBoneMembershipApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BareBoneMembershipApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class PingPongController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public PingPongController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("ping")]
        public async Task<IActionResult> Ping()
        {
            var client = _httpClientFactory.CreateClient("PongClient");
            var httpResponseMessage = await client.GetAsync("requestUrl?client=100");

            try
            {
                httpResponseMessage.EnsureSuccessStatusCode();

                var clientResponse =
                    await httpResponseMessage.Content.ReadAsAsync<ApiResponse<Client>>(new List<MediaTypeFormatter>()
                    {
                        new JsonMediaTypeFormatter
                        {
                            SerializerSettings = new JsonSerializerSettings
                            {
                                DateFormatString = "dd/MM/yyyy"
                            }
                        }
                    });
                return Ok(clientResponse);
            }
            finally
            {
                httpResponseMessage.Dispose();
            }
        }
    }
}
