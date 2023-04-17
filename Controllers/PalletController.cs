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

        [HttpGet("GeefLocatieInfoBijPalletnr/{palletNummer}")]
        public async Task<LocatieInfo> GeefLocatieInfoBijPalletnr(string palletNummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefLocatieInfoBijPalletnrAsync(palletNummer);
            if(result != null) result.PalletsOpLocatie = null;//not needed for app.

            return result;


        }


        [HttpPost("SluitVerzamelpallet/{palletNumber}/{productionNumber}")]
        public async Task<string> SluitVerzamelpallet(string palletNumber, int? productionNumber = 0)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.PickpalletDoelLocatieNaamVerzamelAanvoerOpdrachtAsync(palletNumber, productionNumber);
            return result;
        }
       

    }
}
