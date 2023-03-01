using TruckWebService;

namespace LESAPI.Helpers
{
    public static class Mapper
    {
        public static PalletInfo MapPalletInfo(ResultaatOfPalletInfo5SlwlhPY palletInfor)
        {
            var palletinfo = new PalletInfo();
            if (palletInfor.ResultaatObject == null) return palletinfo;
            palletinfo.AantalColli = palletInfor.ResultaatObject.AantalColli;
            palletinfo.ArtikelNaam = palletInfor.ResultaatObject.ArtikelNaam;
            palletinfo.BeladingInfo = palletInfor.ResultaatObject.BeladingInfo;
            palletinfo.BeladingsGewicht = palletInfor.ResultaatObject.BeladingsGewicht;
            palletinfo.LastdragerNaam = palletInfor.ResultaatObject.LastdragerNaam;
            palletinfo.OpdrachtInfo = palletInfor.ResultaatObject.OpdrachtInfo;
            palletinfo.PalletLocatie = palletInfor.ResultaatObject.PalletLocatie;
            palletinfo.PalletLocatieNr = palletInfor.ResultaatObject.PalletLocatieNr;
            palletinfo.Palletnummer = palletInfor.ResultaatObject.Palletnummer;

            //en de rest is nog niet belangrijk komt nog.
            return palletinfo;
        }
    }
}
