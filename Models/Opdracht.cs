using TruckWebService;

using System.Runtime.Serialization;

    public class LocalOpdracht
    {
        public OpdrachtTypes Type { get; set; }

        /// <summary>
        /// Gets or sets de status van de opdracht.
        /// </summary>
        /// <value>De status van de opdracht.</value>
        public OpdrachtStatuss Status { get; set; }

        /// <summary>
        /// Gets or sets het opdrachtnummer.
        /// </summary>
        /// <value>Het opdrachtnummer.</value>
        public string OpdrachtNummer { get; set; }

        /// <summary>
        /// Gets or sets een indicatie of dit een oude opdracht is.
        /// </summary>
        /// <value><c>true</c> als dit een oude opdracht is, anders <c>false</c>.</value>
        public bool IsOudeOpdracht { get; set; }

        /// <summary>
        /// Gets or sets het beginlocatienummer.
        /// </summary>
        /// <value>Het beginlocatienummer.</value>
        public string BeginLocatieNr { get; set; }

        /// <summary>
        /// Gets or sets het beginlocatienummer-display.
        /// </summary>
        /// <value>Het beginlocatienummer-display.</value>
        public string BeginLocatieNrDisplay { get; set; }

        /// <summary>
        /// Gets or sets eindlocatienummer.
        /// </summary>
        /// <value>Het eindlocatienummer.</value>
        public string EindLocatieNr { get; set; }

        /// <summary>
        /// Gets or sets het eindlocatienummer-display.
        /// </summary>
        /// <value>Het eindlocatienummer-display.</value>
        public string EindLocatieNrDisplay { get; set; }

        /// <summary>
        /// Gets or sets het controlenummer van een locatie.
        /// </summary>
        /// <value>Het controlenummer van een locatie.</value>
        public string ControleNummer { get; set; }

        /// <summary>
        /// Gets or sets het palletnummer van de beginlocatie.
        /// </summary>
        /// <value>Het palletnummer van de beginlocatie.</value>
        public string PalletNummerVan { get; set; }

        /// <summary>
        /// Gets or sets het palletnummer van de eindlocatie.
        /// </summary>
        /// <value>Het palletnummer van de eindlocatie.</value>
        public string PalletNummerNaar { get; set; }

        /// <summary>
        /// Gets or sets het aantal colli.
        /// </summary>
        /// <value>Het aantal colli.</value>
        public int AantalColli { get; set; }

        /// <summary>
        /// Gets or sets de artikelnaam.
        /// </summary>
        /// <value>De artikelnaam.</value>
        public string Artikel { get; set; }

        /// <summary>
        /// Gets or sets het artikelnummer.
        /// </summary>
        /// <value>Het artikelnummer.</value>
        public string ArtikelNr { get; set; }

        /// <summary>
        /// Gets or sets het zonegroepnummer.
        /// </summary>
        /// <value>Het zonegroepnummer.</value>
        public string ZonegroepNr { get; set; }

        /// <summary>
        /// Gets or sets de lastdragernaam.
        /// </summary>
        /// <value>De lastdragernaam.</value>
        public string LastDrager { get; set; }

        /// <summary>
        /// Gets or sets een indicatie of de bestemming een picklocatie is.
        /// </summary>
        /// <value><c>true</c> indien de bestemming een picklocatie is; anders <c>false</c>.</value>
        public bool IsBestemmingPickLocatie { get; set; }

        /// <summary>
        /// Gets or sets een indicatie of de bestemming een ander gebied is.
        /// </summary>
        /// <value><c>true</c> indien bestemming een ander gebied is; anders <c>false</c>.</value>
        public bool IsBestemmingAnderGebied { get; set; }

        /// <summary>
        /// Gets or sets het gebied.
        /// </summary>
        /// <value>Het gebied.</value>
        public string Gebied { get; set; }

        /// <summary>
        /// Gets or sets de omschrijving van de status.
        /// </summary>
        /// <value>De somschrijving van de status.</value>
        public string StatusOmschrijving { get; set; }

        /// <summary>
        /// Gets or sets de pincode van de operator die de opdracht in behandeling heeft.
        /// </summary>
        /// <value>De pincode van de operator die de opdracht in behandeling heeft.</value>
        public string InbehandelingDoorPincode { get; set; }

        /// <summary>
        /// Gets or sets het aantal afgeronde opdrachten.
        /// </summary>
        /// <value>Het aantal afgeronde opdrachten.</value>
        public int OpdrachtenAfgerond { get; set; }

        /// <summary>
        /// Gets or sets het aantal openstaande opdrachten.
        /// </summary>
        /// <value>Het aantal openstaande opdrachten.</value>
        public int OpdrachtenOpen { get; set; }

        /// <summary>
        /// Gets or sets het idnr van de partij.
        /// </summary>
        /// <value>Het idnr van de partij.</value>
        public string Idnr { get; set; }

        /// <summary>
        /// Gets or sets het gewicht.
        /// </summary>
        /// <value>Het gewicht.</value>
        public double Gewicht { get; set; }

        /// <summary>
        /// Gets or sets het aantal openstaande opdrachten voor een order.
        /// </summary>
        /// Gets or sets het aantal openstaande opdrachten voor een order.
        public int OpdrachtenOpenOrder { get; set; }

        /// <summary>
        /// Gets or sets een optioneel tweede controlenummer.
        /// </summary>
        /// <value>Een optioneel tweede controlenummer.</value>
        public string ControleNummer2 { get; set; }

        /// <summary>
        /// Gets or sets de status van de partij.
        /// </summary>
        /// <value>De status van de partij.</value>
        public PartijStatuss PartijStatus { get; set; }
    }

    public enum PartijStatuss
    {
        /// <summary>
        /// The ok
        /// </summary>
        Ok,
        /// <summary>
        /// The partij onbekend
        /// </summary>
        PartijOnbekend,
        /// <summary>
        /// The partij afgekeurd
        /// </summary>
        PartijAfgekeurd,
        /// <summary>
        /// The artikel niet nodig
        /// </summary>
        ArtikelNietNodig,
        /// <summary>
        /// The artikel onbekend
        /// </summary>
        ArtikelOnbekend,
        /// <summary>
        /// The geen picklocatie
        /// </summary>
        GeenPicklocatie,
        /// <summary>
        /// The geen pallet
        /// </summary>
        GeenPallet,
        /// <summary>
        /// The pallet geen belading
        /// </summary>
        PalletGeenBelading,
        /// <summary>
        /// The pallet leeg
        /// </summary>
        PalletLeeg,
        /// <summary>
        /// The partij is in quarantaine
        /// </summary>
        PartijInQuarantaine
    }

    /// <summary>
    /// Enum OpdrachtType
    /// </summary>
    public enum OpdrachtTypes
    {
        /// <summary>
        /// Type Inslag
        /// </summary>
        Inslag,

        /// <summary>
        /// Type Uitslag
        /// </summary>
        Uitslag,

        /// <summary>
        /// Type  Tel
        /// </summary>
        Tel,

        /// <summary>
        /// Type Aanvul
        /// </summary>
        Aanvul,

        /// <summary>
        /// Type  Transport
        /// </summary>
        Transport,

        /// <summary>
        /// Type  Retour
        /// </summary>
        Retour,

        /// <summary>
        /// Type  GTM
        /// </summary>
        GTM,

        /// <summary>
        /// Type  Fabriek
        /// </summary>
        Fabriek,

        /// <summary>
        /// Type  Weegkeuken
        /// </summary>
        Weegkeuken
    }

    public enum OpdrachtStatuss
    {
        /// <summary>
        /// The beschikbaar
        /// </summary>
        [EnumMember]
        Beschikbaar,
        /// <summary>
        /// The pallet onbekend
        /// </summary>
        [EnumMember]
        PalletOnbekend,
        /// <summary>
        /// The reeds in magazijn
        /// </summary>
        [EnumMember]
        ReedsInMagazijn,
        /// <summary>
        /// The niet beschikbaar
        /// </summary>
        [EnumMember]
        NietBeschikbaar,
        /// <summary>
        /// The buiten gebied
        /// </summary>
        [EnumMember]
        BuitenGebied,
        /// <summary>
        /// The geen order gevonden
        /// </summary>
        [EnumMember]
        GeenOrderGevonden,
        /// <summary>
        /// The order geblokkeerd
        /// </summary>
        [EnumMember]
        OrderGeblokkeerd,
        /// <summary>
        /// The beginlocatie onbekend
        /// </summary>
        [EnumMember]
        BeginlocatieOnbekend,
        /// <summary>
        /// The eindlocatie onbekend
        /// </summary>
        [EnumMember]
        EindlocatieOnbekend,
        /// <summary>
        /// The eindlocatie bezet
        /// </summary>
        [EnumMember]
        EindlocatieBezet,
        /// <summary>
        /// The pallet geen beladingsregels
        /// </summary>
        [EnumMember]
        PalletGeenBeladingsregels,
        /// <summary>
        /// The artikel onbekend
        /// </summary>
        [EnumMember]
        ArtikelOnbekend,
        /// <summary>
        /// The geen inslag
        /// </summary>
        [EnumMember]
        GeenInslag
    }
