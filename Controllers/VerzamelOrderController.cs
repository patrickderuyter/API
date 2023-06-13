using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using TruckWebService;

    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class VerzamelOrderController : ControllerBase
    {
        private readonly ILogger<VerzamelOrderController> logger;

        public VerzamelOrderController(ILogger<VerzamelOrderController> logger)
        {
            this.logger = logger;

        }

        [HttpGet("GeefOpenopdrachten/{pincode}")]
        public async Task<List<AreaProcesssegment>> GeefOpenopdrachten(string pincode)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefProcessegmentenOpenstaandeOrderopdrachtenAsync(0, pincode);

                var areaProcesssegments = result.ResultaatObject.ToList();

                return areaProcesssegments;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<AreaProcesssegment>();
            }
        }

        [HttpGet("GeefProcessegmentenOpenstaandeOrderopdrachten/{processSegmentNumber}/{pincode}")]
        public async Task<List<AreaProcesssegment>> GeefOpenopdrachten(int processSegmentNumber, string pincode)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefProcessegmentenOpenstaandeOrderopdrachtenAsync(processSegmentNumber, pincode);

                var areaProcesssegments = result.ResultaatObject.ToList();

                return areaProcesssegments;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<AreaProcesssegment>();
            }
        }

        [HttpGet("GeefVrijeOrderAanvoerOpdrachten/{areanummer}/{processsegmentnummer}/{pincode}")]
        public async Task<List<OrderAanvoerOpdracht>> GeefVrijeOrderAanvoerOpdrachten(int areanummer, int processsegmentnummer, string pincode)
        {
            try
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
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<OrderAanvoerOpdracht>();
            }
        }

        [HttpPost("BepaalGrondstofVoorraadVoorOpdrachten")]
        public async Task<List<GrondstofVoorraad>> BepaalGrondstofVoorraadVoorOpdrachten([FromBody] long[] opdrachtIds)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.BepaalGrondstofVoorraadVoorOpdrachtenAsync(opdrachtIds);
                return result.ResultaatObject.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<GrondstofVoorraad>();
            }
        }

        [HttpPut("StartOrderOpdracht/{ordernummer}")]
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

        [HttpGet("GeefPickOrder/{ordernummer}/{gebiedscode}")]
        public async Task<PickOrder?> GeefPickOrder(string ordernummer, string gebiedscode)

        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefPickOrderAsync(ordernummer, gebiedscode);
                return !result.Orderregels.Any() ? null : result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new PickOrder();
            }
        }

        [HttpGet("GeefVrijeGTMGebieden/{pincode}")]
        public async Task<List<GTMGebiedInfo>> GeefVrijeGTMGebieden(string pincode)

        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefVrijeGTMGebiedenAsync(pincode);
                return result == null ? new List<GTMGebiedInfo>() : result.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<GTMGebiedInfo>();
            }
        }

        [HttpPut("KoppelGTMGebied/{gebiedId}/{pincode}")]
        public async Task<GTMGebiedInfo> KoppelGTMGebied(long gebiedId, string pincode)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.KoppelGTMGebiedAsync(gebiedId, pincode);
                return result == null ? new GTMGebiedInfo() : result.ResultaatObject;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new GTMGebiedInfo();
            }
        }

        [HttpPost("GeefOrderAanvoerOpdrachtRegels")]
        public async Task<List<OrderAanvoerOpdrachtRegel>> GeefOrderAanvoerOpdrachtRegels(long[] ids)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefOrderAanvoerOpdrachtRegelsAsync(ids);

                var result2 = result?.Where(x => x.Status != "1" && x.Status != "2");

                return result2 == null ? new List<OrderAanvoerOpdrachtRegel>() : result2.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<OrderAanvoerOpdrachtRegel>();
            }
        }

        [HttpPut("ZetGmagPickPallet/{palletNummer}/{pin}/{aanvoeropdrachtId}")]
        public async Task<PalletInfo> ZetGmagPickPallet(string palletNummer, string pin, long aanvoeropdrachtId)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.ZetGmagPickPalletAsync(palletNummer, pin, aanvoeropdrachtId);
                return result.ResultaatObject;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new PalletInfo();
            }
        }

        [HttpPut("AfrondenOrderAanvoeropdrachtRegelGmag")]
        public async Task<ResultaatOfScanResultaat5SlwlhPY> AfrondenOrderAanvoeropdrachtRegelGmagAfrondenOrderAanvoeropdrachtRegelGmag(OrderAanvoerOpdrachtRegel orderAanvoerOpdrachtRegel)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.AfrondenOrderAanvoeropdrachtRegelGmagAsync(orderAanvoerOpdrachtRegel);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var newResult = new ResultaatOfScanResultaat5SlwlhPY();
                newResult.IsValide = false;
                newResult.Foutmelding = ex.Message;
                return newResult;
            }
        }

        [HttpPut("ZetGTMAanvoerverzoeken/{gebiedId}")]
        public async Task<Resultaat> ZetGTMAanvoerverzoeken(long[] supplyRequestIds, long gebiedId)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.ZetGTMAanvoerverzoekenAsync(supplyRequestIds, gebiedId);
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

        [HttpPut("PickpalletAfrondenVerzamelAanvoerOpdracht/{palletnumber}/{truck}/{pin}/{productionNumber}")]
        public async Task<bool> PickpalletAfrondenVerzamelAanvoerOpdracht(string palletnumber, string truck, string pin, int? productionNumber)
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

        [HttpPut("ZetGTMAfvoerverzoeken/{areaId}/{allLines}")]
        public async Task<Resultaat> ZetGTMAfvoerverzoeken(long areaId, bool allLines)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.ZetGTMAfvoerverzoekenAsync(areaId, allLines);
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

        [HttpPut("ZetGTMAanvoerverzoeken/{areaId}")]
        public async Task<Resultaat> ZetGTMAanvoerverzoeken(long areaId, long[] orderIds)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.ZetGTMAanvoerverzoekenAsync(orderIds, areaId);
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

        [HttpPut("GeefGTMGebiedVrij/{areaId}/{pin}")]
        public async Task<ResultaatOfGTMGebiedInfo5SlwlhPY> GeefGTMGebiedVrij(long areaId, string pin)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefGTMGebiedVrijAsync(areaId, pin);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var newResult = new ResultaatOfGTMGebiedInfo5SlwlhPY();
                newResult.IsValide = false;
                newResult.Foutmelding = ex.Message;
                return newResult;
            }
        }

        [HttpPost("StartOrderAanvoerOpdrachtRegel/{findNext}/{pin}/{gtm}/{exclusivePalletNumber?}/{idnr?}")]
        public async Task<ResultaatOfOrderAanvoerOpdrachtRegel5SlwlhPY> StartOrderAanvoerOpdrachtRegel(OrderAanvoerOpdrachtRegel line,
            bool findNext, string pin, bool gtm, string? exclusivePalletNumber = null, string? idnr = null)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.StartOrderAanvoerOpdrachtRegelAsync(line,
                    exclusivePalletNumber, idnr, findNext, pin, gtm);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                var newResult = new ResultaatOfOrderAanvoerOpdrachtRegel5SlwlhPY();
                newResult.IsValide = false;
                newResult.Foutmelding = ex.Message;
                return newResult;
            }
        }

        [HttpPut("AnnuleerOrderAanvoerOpdrachtRegel")]
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