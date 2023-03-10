using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class VerplaatsingenController : ControllerBase
    {
        private readonly ILogger<VerplaatsingenController> _logger;

        public VerplaatsingenController(ILogger<VerplaatsingenController> logger)
        {
            _logger = logger;

        }

        [HttpGet("GeefBeschikbareRijenVoorLocatie/{locatie}/{palletnummer}")]
        public async Task<LocatiePositie> GeefBeschikbareRijenVoorLocatie(string locatie,string palletnummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient
                .GeefBeschikbareRijenVoorLocatieAsync(locatie, palletnummer);
            return result.ResultaatObject;
        }

        [HttpPost("StartMAMOpdracht/{pincode}/{gebied}/{palletnummer}")]
        public async Task<Resultaat> StartMAMOpdracht(string pincode, string gebied, string palletnummer)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient
                .StartMAMOpdrachtAsync(pincode,  gebied, palletnummer);
            return result;
        }

        [HttpPost("VerwerkMAMOpdracht/{pincode}/{palletnummer}/{locatieid}")]
        public async Task<Resultaat> VerwerkMAMOpdracht(string pincode, string palletnummer, long locatieid)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.VerwerkMAMOpdrachtAsync(
                pincode, palletnummer, locatieid,true);
            return result;
        }
        
    }
}
