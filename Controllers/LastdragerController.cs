using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TruckWebService;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LastdragerController : ControllerBase
    {
        private readonly ILogger<LastdragerController> _logger;

        public LastdragerController(ILogger<LastdragerController> logger)
        {
            _logger = logger;

        }

        [HttpGet("GeefLastdragers")]
        public async Task<List<LastdragerDC>> GeefLastdragers()
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefLastdragersAsync();

            return result.ResultaatObject.ToList();


        }
    }
}
