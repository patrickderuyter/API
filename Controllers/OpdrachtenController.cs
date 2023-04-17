namespace LESAPI.Controllers
{
    using Helpers;
    using Microsoft.AspNetCore.Mvc;
    using Models;
    using OpdrachtService;
    using TruckWebService;
    using Opdracht = OpdrachtService.Opdracht;
    using Resultaat = OpdrachtService.Resultaat;
    using ResultaatOfOpdracht5SlwlhPY = OpdrachtService.ResultaatOfOpdracht5SlwlhPY;

    [ApiController]
    [Route("[controller]")]
    public class OpdrachtenController : ControllerBase
    {
        private readonly ILogger<OpdrachtenController> _logger;

        public OpdrachtenController(ILogger<OpdrachtenController> logger)
        {
            _logger = logger;

        }

        [HttpGet( "AantalOpenstaandeOpdrachtenGMAG")]
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

        [HttpGet("CheckPalletTelOpdracht/{palletnummer}")]
        public async Task<PalletInfo?> CheckPalletTelOpdracht(string palletnummer)
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
        public async Task<Opdracht> StartAanvulOpdracht
            (string pin, Opdracht opdracht)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = serviceClient.StartAanvulOpdrachtAsync(pin, opdracht);
            return result.Result.ResultaatObject;

        }

        [HttpGet("GeefVolgendeOpdracht/{trucknummer}")]
        public async Task<Opdracht> GeefVolgendeOpdracht(string trucknummer)
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

        [HttpGet("GeefProcessegmentenBijFabriekopdrachten/{pin}")]
        public async Task<Processegment[]> GeefProcessegmentenBijFabriekopdrachten(string pin)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await 
                serviceClient.GeefProcessegmentenBijFabriekopdrachtenAsync(pin);
            return result.ResultaatObject;
        }

        [HttpPost("GeefVrijeFabriekopdrachtenVoorFabrieken/{pin}")]
        public async Task<FabriekOpdracht[]> GeefVrijeFabriekopdrachtenVoorFabrieken(string pin, [FromBody] List<int> processegmentCodes)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await
                serviceClient.GeefVrijeFabriekopdrachtenVoorFabriekenAsync(pin, processegmentCodes.ToArray());
            return result.ResultaatObject;
        }

        [HttpGet("GeefOrderaanvoeropdrachtIdVoorPallet/{palletnummer}")]
        public async Task<long> GeefOrderaanvoeropdrachtIdVoorPallet(string palletnummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await
                serviceClient.GeefOrderaanvoeropdrachtIdVoorPalletAsync(palletnummer);
            return result.ResultaatObject;
        }

        [HttpPut("FabriekopdrachtStarten/{orderaanvoerOpdrachtId}/{gebied}/{pin}")]
        public async Task<Opdracht> FabriekopdrachtStarten(long orderaanvoerOpdrachtId, string gebied, string pin)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await serviceClient.FabriekopdrachtStartenAsync(orderaanvoerOpdrachtId, gebied, pin);
            return result.ResultaatObject;
        }

        [HttpPut("FabriekopdrachtAfronden/{opdrachtNummer}/{pin}")]
        public async Task<Resultaat> FabriekopdrachtAfronden(string opdrachtNummer, string pin)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await
                serviceClient.FabriekopdrachtAfrondenAsync(opdrachtNummer,  pin);
            return result;
        }

        [HttpPut("FabriekopdrachtAnnuleren/{opdrachtNummer}/{pin}")]
        public async Task<Resultaat> FabriekopdrachtAnnuleren(string opdrachtNummer, string pin)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await
                serviceClient.FabriekopdrachtAnnulerenAsync(opdrachtNummer, pin);
            return result;
        }

        [HttpPut("StartInslagOpdrachtLocatieBepalingLES/{pin}/{palletNumber}/{area}")]
        public async Task<ResultaatOfOpdracht5SlwlhPY> StartInslagOpdrachtLocatieBepalingLES(string pin,string palletNumber, string area, bool locationDetermination)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await
                serviceClient.StartInslagOpdrachtLocatieBepalingLESAsync(pin, palletNumber, area, locationDetermination);
            return result;
        }

        [HttpPut("StartInslagOpdracht/{pin}/{palletNumber}/{area}")]
        public async Task<ResultaatOfOpdracht5SlwlhPY> StartInslagOpdracht(string pin, string palletNumber, string area)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await
                serviceClient.StartInslagOpdrachtAsync(pin, palletNumber, area);
            return result;
        }

        [HttpPut("InslagOpdrachtAfronden")]
        public async Task<bool> InslagOpdrachtAfronden(Opdracht opdracht)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.InslagOpdrachtAfrondenAsync(opdracht);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPut("StartUitslagOpdracht/{pin}")]
        public async Task<ResultaatOfOpdracht5SlwlhPY> StartUitslagOpdracht(string pin, Opdracht opdracht)
        {
            await using var serviceClient = new OpdrachtServiceClient();
            var result = await
                serviceClient.StartUitslagOpdrachtAsync(pin, opdracht);
            return result;
        }

        [HttpPut("UitslagOpdrachtAfronden")]
        public async Task<bool> UitslagOpdrachtAfronden(Opdracht opdracht)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.UitslagOpdrachtAfrondenAsync(opdracht);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPut("UitslagOpdrachtMarkeren/{orderNumber}")]
        public async Task<bool> UitslagOpdrachtMarkeren(string orderNumber)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.UitslagOpdrachtMarkerenAsync(orderNumber);
            }
            catch
            {
                return false;
            }
            return true;
        }

        [HttpPost("StartGTMOpdracht/{pin}")]
        public async Task<ResultaatOfOpdracht5SlwlhPY> StartGTMOpdracht(string pin, Opdracht opdracht)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                var result = await serviceClient.StartGTMOpdrachtAsync(pin,opdracht);
                return result;
            }
            catch
            {
                return new ResultaatOfOpdracht5SlwlhPY();
            }
        }

        [HttpPut("GTMOpdrachtAfronden")]
        public async Task<bool> GTMOpdrachtAfronden(Opdracht opdracht)
        {
            try
            {
                await using var serviceClient = new OpdrachtServiceClient();
                await serviceClient.GTMOpdrachtAfrondenAsync(opdracht);
                return true;
            }
            catch
            {
                return false;
            }
        }

        

    }
}
