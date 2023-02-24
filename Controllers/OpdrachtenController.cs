using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using LESAPI.Models;
    using TruckWebService;

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

    }
}
