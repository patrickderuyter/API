namespace LESAPI.Models
{
    using TruckWebService;

    public class ReceiptRegistrationWithNumber
    {
        public ReceiptRegistrationWithNumber(OntvangstregistratiePallet receiptRegistration, string trucknumber)
        {
            ReceiptRegistration = receiptRegistration;
            Trucknumber = trucknumber;
        }

        public OntvangstregistratiePallet ReceiptRegistration { get; set; }
        public string Trucknumber { get; set; }
    }
}
