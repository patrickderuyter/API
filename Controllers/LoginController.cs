using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LastdragerController> _logger;

        public LoginController(ILogger<LastdragerController> logger)
        {
            _logger = logger;

        }

        
        [HttpGet("Inloggen/{truckNumber}/{pin}")]
        public async Task<LoginResultaat> Inloggen(string truckNumber, string pin)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.InloggenAsync(truckNumber, pin);
            return result;
        }


        [HttpGet("GeefTruckConfiguratie/{truck}")]
        public async Task<TruckSettings> GeefTruckConfiguratie(string truck)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefTruckConfiguratieAsync(truck);
            return result;
        }
    }
}
