using Microsoft.AspNetCore.Mvc;
using TruckWebService;

namespace LESAPI.Controllers
{
    using Models;
    using System.Text;
    using System.Text.Json;

    [ApiController]
    [Route("[controller]")]
    public class OntvangstRegistratiePalletController : ControllerBase
    {
        private readonly ILogger<OntvangstRegistratiePalletController> _logger;

        public OntvangstRegistratiePalletController(ILogger<OntvangstRegistratiePalletController> logger)
        {
            _logger = logger;

        }
        [HttpGet("{ontvangstordernr},{palletnr}")]
        public async Task<OntvangstregistratiePallet> GeefOntvangstregistratiePallet(string ontvangstordernr, string palletnr)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefOntvangstregistratiePalletAsync(ontvangstordernr, palletnr);
            return result.ResultaatObject;
        }

        [HttpPost("AanmakenOntvangstorderPallets/{gebiedcode}, {ontvangstordernummer}, {aantal}")]
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
                serviceClient.OpslaanOntvangstregistratiePallet(receiptRegistrationWithNumber.ReceiptRegistration, receiptRegistrationWithNumber.Trucknumber);
            return result;
        }
    }
}
