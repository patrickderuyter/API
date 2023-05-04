namespace LESAPI.Helpers
{
    //using Models;
    using TruckWebService;

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

        //public static OpdrachtService.Opdracht LocalOpdachtToServiceOpdracht(LocalOpdracht opdracht)
        //{
        //    var newOS = new OpdrachtService.Opdracht();
        //    newOS.AantalColli = opdracht.AantalColli;
        //    newOS.Artikel = opdracht.Artikel;
        //    newOS.ArtikelNr = opdracht.ArtikelNr;
        //    newOS.BeginLocatieNr = opdracht.BeginLocatieNr;
        //    newOS.BeginLocatieNrDisplay = opdracht.BeginLocatieNrDisplay;
        //    newOS.ControleNummer = opdracht.ControleNummer;
        //    newOS.ControleNummer2 = opdracht.ControleNummer2;
        //    newOS.EindLocatieNr = opdracht.EindLocatieNr;
        //    newOS.EindLocatieNrDisplay = opdracht.EindLocatieNrDisplay;
        //    newOS.Gebied = opdracht.Gebied;
        //    newOS.Gewicht = opdracht.Gewicht;
        //    newOS.Idnr = opdracht.Idnr;
        //    newOS.InbehandelingDoorPincode = opdracht.InbehandelingDoorPincode;
        //    newOS.IsBestemmingAnderGebied = opdracht.IsBestemmingAnderGebied;
        //    newOS.IsBestemmingPickLocatie = opdracht.IsBestemmingPickLocatie;
        //    newOS.IsOudeOpdracht = opdracht.IsOudeOpdracht;
        //    newOS.LastDrager = opdracht.LastDrager;
        //    newOS.OpdrachtNummer = opdracht.OpdrachtNummer;
        //    newOS.OpdrachtenAfgerond = opdracht.OpdrachtenAfgerond;
        //    newOS.OpdrachtenOpen = opdracht.OpdrachtenOpen;
        //    newOS.OpdrachtenOpenOrder = opdracht.OpdrachtenOpenOrder;
        //    newOS.PalletNummerNaar = opdracht.PalletNummerNaar;
        //    newOS.PalletNummerVan = opdracht.PalletNummerVan;
        //    //newOS.PartijStatus = (PartijStatus)opdracht.PartijStatus;
        //    //newOS.Status = (OpdrachtStatus )opdracht.Status;
        //    newOS.StatusOmschrijving = opdracht.StatusOmschrijving;
        //    //newOS.Type = (OpdrachtType)opdracht.Type;
        //    newOS.ZonegroepNr = opdracht.ZonegroepNr;
        //    return newOS;
        //}

        //public static LocalOpdracht ServiceOpdrachtToLocalOpdacht(OpdrachtService.Opdracht opdracht)
        //{
        //    var newOS = new LocalOpdracht();
        //    newOS.AantalColli = opdracht.AantalColli;
        //    newOS.Artikel = opdracht.Artikel;
        //    newOS.ArtikelNr = opdracht.ArtikelNr;
        //    newOS.BeginLocatieNr = opdracht.BeginLocatieNr;
        //    newOS.BeginLocatieNrDisplay = opdracht.BeginLocatieNrDisplay;
        //    newOS.ControleNummer = opdracht.ControleNummer;
        //    newOS.ControleNummer2 = opdracht.ControleNummer2;
        //    newOS.EindLocatieNr = opdracht.EindLocatieNr;
        //    newOS.EindLocatieNrDisplay = opdracht.EindLocatieNrDisplay;
        //    newOS.Gebied = opdracht.Gebied;
        //    newOS.Gewicht = opdracht.Gewicht;
        //    newOS.Idnr = opdracht.Idnr;
        //    newOS.InbehandelingDoorPincode = opdracht.InbehandelingDoorPincode;
        //    newOS.IsBestemmingAnderGebied = opdracht.IsBestemmingAnderGebied;
        //    newOS.IsBestemmingPickLocatie = opdracht.IsBestemmingPickLocatie;
        //    newOS.IsOudeOpdracht = opdracht.IsOudeOpdracht;
        //    newOS.LastDrager = opdracht.LastDrager;
        //    newOS.OpdrachtNummer = opdracht.OpdrachtNummer;
        //    newOS.OpdrachtenAfgerond = opdracht.OpdrachtenAfgerond;
        //    newOS.OpdrachtenOpen = opdracht.OpdrachtenOpen;
        //    newOS.OpdrachtenOpenOrder = opdracht.OpdrachtenOpenOrder;
        //    newOS.PalletNummerNaar = opdracht.PalletNummerNaar;
        //    newOS.PalletNummerVan = opdracht.PalletNummerVan;
        //    //newOS.PartijStatus = (global::PartijStatus)opdracht.PartijStatus;
        //    //newOS.Status = (global::OpdrachtStatus)opdracht.Status;
        //    newOS.StatusOmschrijving = opdracht.StatusOmschrijving;
        //    //newOS.Type = (global::OpdrachtType)opdracht.Type;
        //    newOS.ZonegroepNr = opdracht.ZonegroepNr;
        //    return newOS;
        //}
    }
}
