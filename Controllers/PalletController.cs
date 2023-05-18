namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using TruckWebService;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class PalletController : ControllerBase
    {
        private readonly ILogger<PalletController> logger;

        public PalletController(ILogger<PalletController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("GeefLocatieInfoBijPalletnr/{palletNummer}")]
        public async Task<LocatieInfo> GeefLocatieInfoBijPalletnr(string palletNummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefLocatieInfoBijPalletnrAsync(palletNummer);
                if (result != null) result.PalletsOpLocatie = null;//not needed for app.
                result ??= new LocatieInfo();
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LocatieInfo();
            }
        }

        [HttpPut("SluitVerzamelpallet/{palletNumber}/{productionNumber}")]
        public async Task<string> SluitVerzamelpallet(string palletNumber, int? productionNumber = 0)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.PickpalletDoelLocatieNaamVerzamelAanvoerOpdrachtAsync(palletNumber, productionNumber);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return string.Empty;
            }
        }
    }
}
