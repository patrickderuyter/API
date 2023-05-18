namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Authorization;
    using TruckWebService;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class VerplaatsingenController : ControllerBase
    {
        private readonly ILogger<VerplaatsingenController> logger;

        public VerplaatsingenController(ILogger<VerplaatsingenController> logger)
        {
            this.logger = logger;
        }

        [HttpGet("GeefBeschikbareRijenVoorLocatie/{locatie}/{palletnummer}")]
        public async Task<LocatiePositie> GeefBeschikbareRijenVoorLocatie(string locatie, string palletnummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient
                    .GeefBeschikbareRijenVoorLocatieAsync(locatie, palletnummer);
                return result.ResultaatObject;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new LocatiePositie();
            }
        }

        [HttpPut("StartMAMOpdracht/{pincode}/{gebied}/{palletnummer}")]
        public async Task<Resultaat> StartMAMOpdracht(string pincode, string gebied, string palletnummer)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient
                    .StartMAMOpdrachtAsync(pincode, gebied, palletnummer);
                return result;
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

        [HttpPut("VerwerkMAMOpdracht/{pincode}/{palletnummer}/{locatieid}")]
        public async Task<Resultaat> VerwerkMAMOpdracht(string pincode, string palletnummer, long locatieid)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.VerwerkMAMOpdrachtAsync(
                    pincode, palletnummer, locatieid, true);
                return result;
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
    }
}
