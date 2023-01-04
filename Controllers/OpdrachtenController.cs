using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class OpdrachtenController : ControllerBase
    {
        private readonly ILogger<OpdrachtenController> _logger;

        public OpdrachtenController(ILogger<OpdrachtenController> logger)
        {
            _logger = logger;

        }

        [HttpGet(Name = "AantalOpenstaandeOpdrachtenGMAG")]
        public async Task<ResultaatOfAantalOpenstaandeOpdrachtenGMAG5SlwlhPY> GetOpdrachtservice()
        {
            using (var serviceClient = new TruckWebServiceClient())
            {
                var result = await serviceClient.GeefAantalOpenstaandeOpdrachtenGMAGAsync();
                return result;
            }


        }
    }
}
