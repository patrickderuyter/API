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

        [HttpGet("GeefAantalOpenstaandeOpdrachtenGMAG/{gebiedcode}")]
        public async Task<IEnumerable<OntvangstOrder>> GeefAantalOpenstaandeOpdrachtenGMAG(string gebiedcode)
        {
            using (var serviceClient = new TruckWebServiceClient())
            {
                var result = await serviceClient.GeefTeVerwerkenOntvangstordersAsync(gebiedcode);
                return result.ResultaatObject;
            }

            
        }

        [HttpGet("GeefOntvangstorderPallets/{ontvangstordernummer}")]
        public async Task<IEnumerable<OntvangstregistratiePallet>> GeefOntvangstorderPallets(string ontvangstordernummer)
        {
            using (var serviceClient = new TruckWebServiceClient())
            {
                var result = await serviceClient.GeefOntvangstorderPalletsAsync(ontvangstordernummer);
                foreach (var regel in result.ResultaatObject)
                {
                    //slechte afhanddeling in de service. Slag nog een keer ophalen.
                    var resultIndivi = await serviceClient.GeefOntvangstregistratiePalletAsync(regel.Ontvangstordernr,regel.Palletnr);

                    regel.Slag = resultIndivi.ResultaatObject.Slag;
                }
                return result.ResultaatObject;
            }
        }
    }
}