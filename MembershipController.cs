using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BareBoneMembershipApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MembershipController : ControllerBase
    {
        private readonly IService _newHealthService;

        public MembershipController(IService newHealthService)
        {
            _newHealthService = newHealthService;
        }

        [HttpGet("{membershipNumber}")]
        public async Task<IActionResult> Get([FromRoute] FetchByClientIdRequest fetchByClientIdRequest)
        {
            var result = await _newHealthService.FetchClient(fetchByClientIdRequest.ClientId);
            return Ok(result);
        }
    }
}
