using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using Helpers;
    using Models;
    using OpdrachtService;
    using TruckWebService;
    using Opdracht = OpdrachtService.Opdracht;

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
        public async Task<AantalOpenstaandeOpdrachtenGMAG> GetOpdrachtservice()
        {
            using (var serviceClient = new TruckWebServiceClient())
            {
                var result = await serviceClient.GeefAantalOpenstaandeOpdrachtenGMAGAsync();
                
                return result.ResultaatObject;
            }


        }

        [HttpGet("GeefLocatieInfoVolgendeLocatie/{huidigelocatie}")]
        public async Task<LocatieInfo> GeefLocatieInfoVolgendeLocatie(string huidigelocatie)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefLocatieInfoVolgendeLocatieAsync(huidigelocatie);

            return result;
        }


        [HttpGet("GeefLocatieInfo/{locatienummer}")]
        public async Task<LocatieInfo> GeefLocatieInfo(string locatienummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefLocatieInfoAsync(locatienummer);

            return result;
        }

        [HttpPost("VerwerkTelOpdracht")]
        public async Task<bool> VerwerkTelOpdracht(LocatieInfoWithPin locatieInfoWithPin)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                await serviceClient.VerwerkTelOpdrachtAsync(locatieInfoWithPin.LocatieInfo, locatieInfoWithPin.Pin);
            }
            catch
            {
                return false;

            }

            return true;
        }

        [HttpGet("GetCheckPalletTelOpdracht/{palletnummer}")]
        public async Task<PalletInfo?> GetCheckPalletTelOpdracht(string palletnummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.CheckPalletTelOpdrachtAsync(palletnummer);
                var palletinfo = Mapper.MapPalletInfo(result);
                return palletinfo;
            }
            catch
            {
                return null;

            }
        }

        [HttpPost("StartAanvulOpdracht/{pin}")]
        public async Task<OpdrachtService.Opdracht> StartAanvulOpdracht
            (string pin, OpdrachtService.Opdracht opdracht)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = serviceClient.StartAanvulOpdrachtAsync(pin, opdracht);
            return result.Result.ResultaatObject;

        }

        [HttpGet("GeefVolgendeOpdracht/{trucknummer}")]
        public async Task<OpdrachtService.Opdracht> GeefVolgendeOpdracht(string trucknummer)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await serviceClient.GeefVolgendeOpdrachtAsync(trucknummer);
            return result;
        }

        [HttpPost("OpdrachtOntkoppelen/{opdrachtnummer}")]
        public async Task<bool> OpdrachtOntkoppelen(string opdrachtnummer)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.OpdrachtOntkoppelenAsync(opdrachtnummer);
                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPost("AanvulOpdrachtAfronden/{colliOpEindlocatie}")]
        public async Task<bool> AanvulOpdrachtAfronden(Opdracht opdracht, int colliOpEindlocatie)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.AanvulOpdrachtAfrondenAsync(opdracht, colliOpEindlocatie);
                //normaal gesproken gaat er dan een bericht uit en wordt de opdracht afgesloten.
                return true;
            }
            catch
            {
                return false;
            }

        }

        [HttpPut("StartTransportOpdracht/{pin}")]
        public async Task<Opdracht> StartTransportOpdracht(string pin,[FromBody] Opdracht opdracht)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                var result = await serviceClient.StartTransportOpdrachtAsync(pin, opdracht);
                return result.ResultaatObject;
            }
            catch
            {
                return new Opdracht();
            }
        }

        [HttpPost("TransportOpdrachtAfronden")]
        public async Task<bool> TransportOpdrachtAfronden(Opdracht opdracht)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.TransportOpdrachtAfrondenAsync(opdracht);
                //normaal gesproken gaat er dan een bericht uit en wordt de opdracht afgesloten.
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("LocatieVrijgeven/{locatieNr}")]
        public async Task<bool> LocatieVrijgeven(string locatieNr)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.LocatieVrijgevenAsync(locatieNr);
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
