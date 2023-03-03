using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class VerzamelOrderController : ControllerBase
    {
        private readonly ILogger<VerzamelOrderController> _logger;

        public VerzamelOrderController(ILogger<VerzamelOrderController> logger)
        {
            _logger = logger;
            
    }

        [HttpGet("GeefOpenopdrachten/{pincode}")]
        public async Task<List<AreaProcesssegment>> GeefOpenopdrachten(string pincode)
        {
            var areaProcesssegments = new List<AreaProcesssegment>();
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefProcessegmentenOpenstaandeOrderopdrachtenAsync(0, pincode);

            areaProcesssegments = result.ResultaatObject.ToList();

            return areaProcesssegments;
        }

        [HttpGet("GeefProcessegmentenOpenstaandeOrderopdrachten/{processSegmentNumber}/{pincode}")]
        public async Task<List<AreaProcesssegment>> GeefOpenopdrachten(int processSegmentNumber, string pincode)
        {
            var areaProcesssegments = new List<AreaProcesssegment>();
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefProcessegmentenOpenstaandeOrderopdrachtenAsync(processSegmentNumber, pincode);

            areaProcesssegments = result.ResultaatObject.ToList();

            return areaProcesssegments;
        }

        [HttpGet("GeefVrijeOrderAanvoerOpdrachten/{areanummer}/{processsegmentnummer}/{pincode}")]
        public async Task<List<OrderAanvoerOpdracht>> GeefVrijeOrderAanvoerOpdrachten(int areanummer, int processsegmentnummer, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            int? localareanummer = null;
            int? localprocesssegmentnummer = null;
            if (areanummer != 0)
            {
                localareanummer = areanummer;
            }
            if (processsegmentnummer != 0)
            {
                localprocesssegmentnummer = processsegmentnummer;
            }


            var result = await serviceClient.GeefVrijeOrderAanvoerOpdrachtenAsync(localareanummer, localprocesssegmentnummer, pincode);

            var orderAanvoerOpdrachts = result;

            return orderAanvoerOpdrachts.ToList();
        }

        [HttpPost("BepaalGrondstofVoorraadVoorOpdrachten")]
        public async Task<List<GrondstofVoorraad>> BepaalGrondstofVoorraadVoorOpdrachten([FromBody]long[] opdrachtIds)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.BepaalGrondstofVoorraadVoorOpdrachtenAsync(opdrachtIds);
            return result.ResultaatObject.ToList();
        }

    }
}