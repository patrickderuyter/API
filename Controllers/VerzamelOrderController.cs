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

    }
}