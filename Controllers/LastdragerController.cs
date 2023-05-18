namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using TruckWebService;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class LastdragerController : ControllerBase
    {
        private readonly ILogger<LastdragerController> logger;

        public LastdragerController(ILogger<LastdragerController> logger)
        {
            this.logger = logger;

        }

        [HttpGet("GeefLastdragers")]
        public async Task<List<LastdragerDC>> GeefLastdragers()
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefLastdragersAsync();
                return result.ResultaatObject.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<LastdragerDC>();
            }
        }
    }
}
