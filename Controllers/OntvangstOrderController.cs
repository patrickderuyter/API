using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class OntvangstOrderController : ControllerBase
    {
        private readonly ILogger<OntvangstOrderController> _logger;

        public OntvangstOrderController(ILogger<OntvangstOrderController> logger)
        {
            _logger = logger;
            
    }

        [HttpGet(Name = "GeefAantalOpenstaandeOpdrachtenGMAG")]
        public async Task<IEnumerable<OntvangstOrder>> Get(string gebiedcode)
        {
            using (var serviceClient = new TruckWebServiceClient())
            {
                var result = await serviceClient.GeefTeVerwerkenOntvangstordersAsync(gebiedcode);
                return result.ResultaatObject;
            }

            
        }
    }
}