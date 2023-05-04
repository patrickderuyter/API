namespace LESAPI.Models
{
    using TruckWebService;

    public class JWTTokenResponse : LoginResultaat
    {
        public string Token { get; set; }
        public DateTime TokenExpiryTime { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}