using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Logging;
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class OntvangstOrderController : ControllerBase
    {
        private readonly ILogger<OntvangstOrderController> logger;

        public OntvangstOrderController(ILogger<OntvangstOrderController> logger)
        {
            this.logger = logger;

        }

        [HttpGet("GeefAantalOpenstaandeOpdrachtenGMAG/{gebiedcode}")]
        public async Task<List<OntvangstOrder>> GeefAantalOpenstaandeOpdrachtenGMAG(string gebiedcode)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefTeVerwerkenOntvangstordersAsync(gebiedcode);
                return result.ResultaatObject.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<OntvangstOrder>();
            }
        }

        [HttpGet("GeefOntvangstorderPallets/{ontvangstordernummer}")]
        public async Task<List<OntvangstregistratiePallet>> GeefOntvangstorderPallets(string ontvangstordernummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefOntvangstorderPalletsAsync(ontvangstordernummer);
                foreach (var regel in result.ResultaatObject)
                {
                    //slechte afhanddeling in de service. Slag nog een keer ophalen.
                    var resultIndivi =
                        await serviceClient.GeefOntvangstregistratiePalletAsync(regel.Ontvangstordernr,
                            regel.Palletnr);

                    regel.Slag = resultIndivi.ResultaatObject.Slag;
                }

                return result.ResultaatObject.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<OntvangstregistratiePallet>();
            }
        }
    }
}