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
        public async Task<Resultaat> AnnuleerEindcontrole(string pallet,string pincode)
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
    }
}
