namespace LESAPI.Models;

using TruckWebService;

public class LocatieInfoWithPin
{
    public LocatieInfoWithPin(LocatieInfo locatieInfo, string pin)
    {
        LocatieInfo = locatieInfo;
        Pin = pin;
    }

    public LocatieInfo LocatieInfo { get; set; }
    public string Pin { get; set; }
}