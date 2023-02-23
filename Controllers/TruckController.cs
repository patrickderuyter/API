using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ILogger<TruckController> _logger;

        public TruckController(ILogger<TruckController> logger)
        {
            _logger = logger;

        }
        [HttpGet("GeefTruckConfiguratie/{trucknummer}")]
        public async Task<TruckSettings> GeefTruckConfiguratie(string trucknummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefTruckConfiguratieAsync(trucknummer);
            return result;
        }

        [HttpPut("OpslaanTruckConfiguratie")]
        public async Task OpslaanTruckConfiguratie(TruckSettings truckSettings)
        {
            await using var serviceClient = new TruckWebServiceClient();
            await serviceClient.OpslaanTruckConfiguratieAsync(truckSettings);
        }
    }
}
