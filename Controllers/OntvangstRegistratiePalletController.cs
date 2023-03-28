using Microsoft.AspNetCore.Mvc;
using TruckWebService;

namespace LESAPI.Controllers
{
    using Models;

    [ApiController]
    [Route("[controller]")]
    public class OntvangstRegistratiePalletController : ControllerBase
    {
        private readonly ILogger<OntvangstRegistratiePalletController> _logger;

        public OntvangstRegistratiePalletController(ILogger<OntvangstRegistratiePalletController> logger)
        {
            _logger = logger;

        }
        [HttpGet("GeefOntvangstregistratiePallet/{ontvangstordernr}/{palletNummer}")]
        public async Task<OntvangstregistratiePallet> GeefOntvangstregistratiePallet(string ontvangstordernr, string palletNummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefOntvangstregistratiePalletAsync(ontvangstordernr, palletNummer);
            return result.ResultaatObject;
        }

        [HttpPost("AanmakenOntvangstorderPallets/{gebiedcode}/{ontvangstordernummer}/{aantal}")]
        public async Task<Resultaat> AanmakenOntvangstorderPallets(string gebiedcode, string ontvangstordernummer, int aantal)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result =
                await serviceClient.AanmakenOntvangstorderPalletsAsync(gebiedcode.Trim(), ontvangstordernummer.Trim(), aantal);
            return result;
        }

        [HttpPost("OpslaanOntvangstregistratiePallet")]
        public async Task<Resultaat> OpslaanOntvangstregistratiePallet(ReceiptRegistrationWithNumber receiptRegistrationWithNumber)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result =
                serviceClient.OpslaanOntvangstregistratiePalletAsync(receiptRegistrationWithNumber.ReceiptRegistration, receiptRegistrationWithNumber.Trucknumber);
            return result.Result;
        }
    }
}
