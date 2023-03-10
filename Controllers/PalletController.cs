using Microsoft.AspNetCore.Mvc;
using TruckWebService;

namespace LESAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PalletController : ControllerBase
    {
        private readonly ILogger<PalletController> _logger;

        public PalletController(ILogger<PalletController> logger)
        {
            _logger = logger;

        }

        [HttpGet("GeefLocatieInfoBijPalletnr/{palletnr}")]
        public async Task<LocatieInfo> GeefLocatieInfoBijPalletnr(string palletnr)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefLocatieInfoBijPalletnrAsync(palletnr);
            result.PalletsOpLocatie = null;//not needed for app.

            return result;


        }


        [HttpPost("SluitVerzamelpallet/{palletnr}/{productienummer}")]
        public async Task<string> SluitVerzamelpallet(string palletnr, int? productienummer = 0)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.PickpalletDoelLocatieNaamVerzamelAanvoerOpdrachtAsync(palletnr, productienummer);
            return result;


        }
    }
}
