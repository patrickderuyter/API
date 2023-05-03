namespace LESAPI
{
    using TruckWebService;

    public class JWTTokenResponse : LoginResultaat
    {
        public string? Token { get; set; }
    }
}