namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using TruckWebService;
    using Microsoft.AspNetCore.Authorization;
    using Models;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class OntvangstRegistratiePalletController : ControllerBase
    {
        private readonly ILogger<OntvangstRegistratiePalletController> logger;

        public OntvangstRegistratiePalletController(ILogger<OntvangstRegistratiePalletController> logger)
        {
            this.logger = logger;

        }
        [HttpGet("GeefOntvangstregistratiePallet/{ontvangstordernr}/{palletNummer}")]
        public async Task<OntvangstregistratiePallet> GeefOntvangstregistratiePallet(string ontvangstordernr, string palletNummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefOntvangstregistratiePalletAsync(ontvangstordernr, palletNummer);
                return result.ResultaatObject;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new OntvangstregistratiePallet();
            }
        }

        [HttpPost("AanmakenOntvangstorderPallets/{gebiedcode}/{ontvangstordernummer}/{aantal}")]
        public async Task<Resultaat> AanmakenOntvangstorderPallets(string gebiedcode, string ontvangstordernummer, int aantal)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result =
                    await serviceClient.AanmakenOntvangstorderPalletsAsync(gebiedcode.Trim(), ontvangstordernummer.Trim(), aantal);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var newResult = new Resultaat
                {
                    IsValide = false,
                    Foutmelding = ex.Message
                };
                return newResult;
            }
        }

        [HttpPut("OpslaanOntvangstregistratiePallet")]
        public async Task<Resultaat> OpslaanOntvangstregistratiePallet(ReceiptRegistrationWithNumber receiptRegistrationWithNumber)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result =
                    serviceClient.OpslaanOntvangstregistratiePalletAsync(receiptRegistrationWithNumber.ReceiptRegistration, receiptRegistrationWithNumber.Trucknumber);
                return result.Result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var newResult = new Resultaat();
                newResult.IsValide = false;
                newResult.Foutmelding = ex.Message;
                return newResult;
            }
        }

        [HttpGet("ProcessGS1LabelSupplier/{receiptNumber}/{palletNumber}/{labelCode}")]
        public async Task<ResultaatOfOntvangstregistratiePalletay1Gw99V> ProcessGS1LabelSupplier(string receiptNumber, string palletNumber, string labelCode)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.VerwerkGS1LabelLeverancierAsync(receiptNumber, palletNumber, labelCode);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var newResult = new ResultaatOfOntvangstregistratiePalletay1Gw99V
                {
                    IsValide = false,
                    Foutmelding = ex.Message
                };
                return newResult;
            }
        }
    }
}
