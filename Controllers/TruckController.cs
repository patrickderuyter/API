namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TruckWebService;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ILogger<TruckController> logger;

        public TruckController(ILogger<TruckController> logger)
        {
            this.logger = logger;

        }
        [HttpGet("GeefTruckConfiguratie/{trucknummer}")]
        public async Task<TruckSettings> GeefTruckConfiguratie(string trucknummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefTruckConfiguratieAsync(trucknummer);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new TruckSettings();
            }
        }

        [HttpPut("OpslaanTruckConfiguratie")]
        public async Task<bool> OpslaanTruckConfiguratie(TruckSettings truckSettings)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                await serviceClient.OpslaanTruckConfiguratieAsync(truckSettings);
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }

        }
    }
}
