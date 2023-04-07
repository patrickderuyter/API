using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using System.Net.NetworkInformation;
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
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefProcessegmentenOpenstaandeOrderopdrachtenAsync(0, pincode);

            var areaProcesssegments = result.ResultaatObject.ToList();

            return areaProcesssegments;
        }

        [HttpGet("GeefProcessegmentenOpenstaandeOrderopdrachten/{processSegmentNumber}/{pincode}")]
        public async Task<List<AreaProcesssegment>> GeefOpenopdrachten(int processSegmentNumber, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefProcessegmentenOpenstaandeOrderopdrachtenAsync(processSegmentNumber, pincode);

            var areaProcesssegments = result.ResultaatObject.ToList();

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


            var result =
                await serviceClient.GeefVrijeOrderAanvoerOpdrachtenAsync(localareanummer, localprocesssegmentnummer,
                    pincode);

            return result.ToList();
        }

        [HttpPost("BepaalGrondstofVoorraadVoorOpdrachten")]
        public async Task<List<GrondstofVoorraad>> BepaalGrondstofVoorraadVoorOpdrachten([FromBody]long[] opdrachtIds)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.BepaalGrondstofVoorraadVoorOpdrachtenAsync(opdrachtIds);
            return result.ResultaatObject.ToList();
        }

        [HttpPost("StartOrderOpdracht/{ordernummer}")]
        public async Task<bool> StartOrderOpdracht(string ordernummer, string pin)

        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                await serviceClient.StartOrderOpdrachtAsync(ordernummer, pin);
            }
            catch 
            {
                return false;
            }

            return true;
        }

        [HttpPost("GeefPickOrder/{ordernummer}/{gebiedscode}")]
        public async Task<PickOrder?> GeefPickOrder(string ordernummer, string gebiedscode)

        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefPickOrderAsync(ordernummer,gebiedscode);
            return !result.Orderregels.Any() ? null : result;
        }


        [HttpGet("GeefVrijeGTMGebieden/{pincode}")]
        public async Task<List<GTMGebiedInfo>> GeefVrijeGTMGebieden(string pincode)

        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefVrijeGTMGebiedenAsync(pincode);
            return result == null ? new List<GTMGebiedInfo>() : result.ToList();
        }

        [HttpPost("KoppelGTMGebied/{gebiedId}/{pincode}")]
        public async Task<GTMGebiedInfo> KoppelGTMGebied(long gebiedId, string pincode)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.KoppelGTMGebiedAsync(gebiedId,pincode);
            return result == null ? new GTMGebiedInfo() : result.ResultaatObject;
        }


        [HttpPost("GeefOrderAanvoerOpdrachtRegels")]
        public async Task<List<OrderAanvoerOpdrachtRegel>> GeefOrderAanvoerOpdrachtRegels(long[] ids)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefOrderAanvoerOpdrachtRegelsAsync(ids);

            var result2 = result?.Where(x => x.Status != "1" && x.Status != "2");

            return result2 == null ? new List<OrderAanvoerOpdrachtRegel>() : result2.ToList();
        }

        [HttpPut("ZetGmagPickPallet/{palletNummer}/{pin}/{aanvoeropdrachtId}")]
        public async Task<PalletInfo> ZetGmagPickPallet(string palletNummer, 
            string pin, long aanvoeropdrachtId)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.ZetGmagPickPalletAsync(palletNummer,pin,aanvoeropdrachtId);
            return result.ResultaatObject;
        }


        [HttpPut("AfrondenOrderAanvoeropdrachtRegelGmag")]
        public async Task<ResultaatOfScanResultaat5SlwlhPY> AfrondenOrderAanvoeropdrachtRegelGmag(OrderAanvoerOpdrachtRegel orderAanvoerOpdrachtRegel)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.AfrondenOrderAanvoeropdrachtRegelGmagAsync(orderAanvoerOpdrachtRegel);
            return result;
        }

        [HttpPut("ZetGTMAanvoerverzoeken/{gebiedId}")]
        public async Task<Resultaat> ZetGTMAanvoerverzoeken(long[] supplyRequestIds, long gebiedId)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.ZetGTMAanvoerverzoekenAsync(supplyRequestIds, gebiedId);
            return result;
        }

        [HttpPut("PickpalletAfrondenVerzamelAanvoerOpdracht/{palletnumber}/{truck}/{pin}/{productionNumber}")]
        public async Task<bool> PickpalletAfrondenVerzamelAanvoerOpdracht(string palletnumber, string truck,string pin, int? productionNumber)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                    await serviceClient.PickpalletAfrondenVerzamelAanvoerOpdrachtAsync(palletnumber, truck, pin,
                        productionNumber);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost("ZetGTMAfvoerverzoeken/{areaId}/{allLines}")]
        public async Task<Resultaat> ZetGTMAfvoerverzoeken(long areaId, bool allLines)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.ZetGTMAfvoerverzoekenAsync(areaId, allLines);
            return result;
        }

        [HttpPost("ZetGTMAanvoerverzoeken/{areaId}")]
        public async Task<Resultaat> ZetGTMAanvoerverzoeken(long areaId, long[] orderIds)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.ZetGTMAanvoerverzoekenAsync(orderIds, areaId);
            return result;
        }

        [HttpPut("GeefGTMGebiedVrij/{areaId}/{pin}")]
        public async Task<ResultaatOfGTMGebiedInfo5SlwlhPY> GeefGTMGebiedVrij(long areaId, string pin)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefGTMGebiedVrijAsync(areaId,pin);
            return result;
        }

        [HttpPut("StartOrderAanvoerOpdrachtRegel/{findNext}/{pin}/{gtm}")]
        [HttpPut("StartOrderAanvoerOpdrachtRegel/{findNext}/{pin}/{gtm}/{exclusivePalletNumber?}/{idnr?}")]
        public async Task<ResultaatOfOrderAanvoerOpdrachtRegel5SlwlhPY> StartOrderAanvoerOpdrachtRegel(OrderAanvoerOpdrachtRegel line,
            bool findNext, string pin, bool gtm, string? exclusivePalletNumber = null, string? idnr = null)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.StartOrderAanvoerOpdrachtRegelAsync(line,
                exclusivePalletNumber, idnr, findNext, pin, gtm);
            return result;
        }

        [HttpPost("AnnuleerOrderAanvoerOpdrachtRegel")]
        public async Task<bool> AnnuleerOrderAanvoerOpdrachtRegel(OrderAanvoerOpdrachtRegel line)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                await serviceClient.AnnuleerOrderAanvoerOpdrachtRegelAsync(line);
                return true;
            }
            catch
            {
                return false;
            }
        }
        


    }
}