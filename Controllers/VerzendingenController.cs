using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class VerzendingenController : ControllerBase
    {
        private readonly ILogger<VerzendingenController> _logger;

        public VerzendingenController(ILogger<VerzendingenController> logger)
        {
            _logger = logger;

        }


        [HttpGet("GeefGmagEindcontrolePallets")]
        public async Task<PalletEindcontrole[]> GeefGmagEindcontrolePallets()
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefGmagEindcontrolePalletsAsync();
            return result.ResultaatObject;
        }

        [HttpGet("GeefFabriekslocatiesBijEindcontrolePallets")]
        public async Task<TruckWebService.Fabriekslocatie[]> GeefFabriekslocatiesBijEindcontrolePallets()
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefFabriekslocatiesBijEindcontrolePalletsAsync();
            return result;
        }

        [HttpPost("AnnuleerEindcontrole/{pallet}/{pincode}")]
        public async Task<Resultaat> AnnuleerEindcontrole(string pallet, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.AnnuleerEindcontroleAsync(pallet, pincode);
            return result;
        }

        [HttpPost("AfrondenEindcontrolePalletnummmer/{pallet}/{pincode}")]
        public async Task<Resultaat> AfrondenEindcontrolePalletnummmer(string pallet, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.AfrondenEindcontrolePalletnummmerAsync(pallet, pincode);
            return result;
        }

        [HttpGet("StartEindcontrole/{pallet}/{pincode}")]
        public async Task<EindcontroleRegel[]> StartEindcontrole(string pallet, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.StartEindcontroleAsync(pallet, pincode);
            return result.ResultaatObject;
        }

        [HttpGet("GeefDczzTeControlerenRitten")]
        public async Task<Rit[]> GeefDczzTeControlerenRitten()
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefDczzTeControlerenRittenAsync();
            return result.ResultaatObject;
        }

        [HttpGet("GeefDczzTeSluitenRitten")]
        public async Task<Rit[]> GeefDczzTeSluitenRitten()
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefDczzTeSluitenRittenAsync();
            return result.ResultaatObject;
        }

        public enum AttachDetach
        {
            Attach,
            Detach
        }

        [HttpPost("KoppelenTruckRun/{runvolgnr}/{trucknr}/{attachDetach}")]
        public async Task<Resultaat> KoppelenTruckRun(string runvolgnr, string trucknr, AttachDetach attachDetach)
        {
            await using var serviceClient = new TruckWebServiceClient();
            if (attachDetach == AttachDetach.Attach)
            {
                var result = await serviceClient.KoppelenTruckRunAsync(runvolgnr, trucknr);
                return result;
            }
            else
            {
                var result = await serviceClient.OntkoppelenTruckRunAsync(runvolgnr, trucknr);
                return result;
            }
        }

        [HttpGet("GetUitslagordersVoorRit/{runvolgnummer}")]
        public async Task<RitOrder[]> GetUitslagordersVoorRit(string runVolgnummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GetUitslagordersVoorRitAsync(runVolgnummer);
            return result.ResultaatObject;
        }

        [HttpPost("AfsluitenRit/{runvolgnr}/{pincode}/{forceerAfsluitenRit}")]
        public async Task<Resultaat> AfsluitenRit(string runvolgnr, string pincode,
            bool forceerAfsluitenRit)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.AfsluitenRitAsync(runvolgnr, pincode, forceerAfsluitenRit);
            return result;
        }

        [HttpGet("GetOpenstaandePalletsVoorUitslagorder/{runvolgnummer}")]
        public async Task<UitslagPalletInfo[]> GetOpenstaandePalletsVoorUitslagorder(string runVolgnummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GetOpenstaandePalletsVoorUitslagorderAsync(runVolgnummer);
            return result.ResultaatObject;
        }

        [HttpGet("GetPalletinfoVoorUitslagorder/{runvolgnummer}/{palletNummer}")]
        public async Task<UitslagPalletInfo> GetPalletinfoVoorUitslagorder(string runVolgnummer, string palletNummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.
                GetPalletinfoVoorUitslagorderAsync(runVolgnummer, palletNummer);
            return result.ResultaatObject;
        }

        [HttpPost("StartVerzendingControle/{palletNummer}/{pin}")]
        public async Task StartVerzendingControle(string palletNummer, string pin)
        {
            await using var serviceClient = new TruckWebServiceClient();
            await serviceClient.
                StartVerzendingControleAsync(palletNummer, pin);
        }

        [HttpPost("VerwerkenVerzendingMeermaligeLeenEmballage/{lastdragernr}/{emballagenr}/{palletNummer}")]
        public async Task<Resultaat> VerwerkenVerzendingMeermaligeLeenEmballage(string lastdragerNummer, string emballageNummer, string palletNummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.
                VerwerkenVerzendingMeermaligeLeenEmballageAsync(lastdragerNummer, emballageNummer, palletNummer);
            return result;
        }

        [HttpPost("VerwerkenScanZegelnummer/{zegelnr}/{palletNummer}")]
        public async Task<Resultaat> VerwerkenScanZegelnummer(string zegelnummer, string palletNummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.
                VerwerkenScanZegelnummerAsync(zegelnummer, palletNummer);
            return result;
        }

        [HttpPost("VerwerkPalletGoedkeuren/{palletNummer}/{ pincode}/{trucknummer}")]
        public async Task<Resultaat> VerwerkPalletGoedkeuren(string palletNummer, string pincode, string trucknummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.
                VerwerkPalletGoedkeurenAsync(palletNummer, pincode, trucknummer);
            return result;
        }

        [HttpPost("VerwerkAantalCorrectie/{palletNummer}/{idNr}/{nieuwAantal}/{verplaatsingsOrderNr}/{pincode}")]
        public async Task<Resultaat> VerwerkAantalCorrectie(string palletNummer, string idNr, int nieuwAantal,
            string verplaatsingsOrderNr, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.
                VerwerkAantalCorrectieAsync(palletNummer, idNr, nieuwAantal, verplaatsingsOrderNr, pincode);
            return result;
        }

        [HttpPost("VerwerkOverstapelen/{runVolgNummer}/{palletNummerVan}/{palletNummerNaar}/{idnr}/{aantal}/{verplaatsingsOrderNummer}/{pincode}")]
        public async Task<Resultaat> VerwerkOverstapelen(string runVolgNummer, string palletNummerVan,
            string palletNummerNaar, string idnr, int aantal,string verplaatsingsOrderNummer ,string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.
                VerwerkOverstapelenAsync(runVolgNummer, palletNummerVan,palletNummerNaar, idnr, aantal, verplaatsingsOrderNummer,pincode);
            return result;
        }




    }
}
