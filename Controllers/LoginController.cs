using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.IdentityModel.Tokens;
    using TruckWebService;

    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LastdragerController> _logger;

        public LoginController(ILogger<LastdragerController> logger)
        {
            _logger = logger;

        }

        
        [HttpGet("Inloggen/{truckNumber}/{pin}")]
        public async Task<JWTTokenResponse> Inloggen(string truckNumber, string pin)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.InloggenAsync(truckNumber, pin);
            if (result.Status == LoginStatus.Geslaagd)
            {
                return CreateToken(result.Gebruiker);
            }

            return new JWTTokenResponse { Token = "", Gebruiker = result.Gebruiker, Status = result.Status,RefreshToken = ""};
        }

        private JWTTokenResponse CreateToken(TruckGebruiker gebruiker)
        {
            var secretKey =
                new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"] ?? string.Empty));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: signinCredentials
            );

           var  refreshToken = GenerateRefreshToken();
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);

            return new JWTTokenResponse
            {
                Token = tokenString,
                Gebruiker = gebruiker,
                RefreshToken = refreshToken,
                RefreshTokenExpiryTime = DateTime.Now.AddDays(1),
                TokenExpiryTime = DateTime.Now.AddMinutes(10)
            };
        }

        [HttpPost("RefreshToken")]
        public async Task<JWTTokenResponse> RefreshToken(JWTTokenResponse originalJwtTokenResponse)
        {
            if (string.IsNullOrWhiteSpace(originalJwtTokenResponse.RefreshToken) 
                && string.IsNullOrWhiteSpace(originalJwtTokenResponse.Token))
            {
                return new JWTTokenResponse { Token = "", Gebruiker = null, Status = LoginStatus.GebruikerOnbekend, RefreshToken = "" };
            }

            return CreateToken(originalJwtTokenResponse.Gebruiker);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        [Authorize]
        [HttpGet("GeefTruckConfiguratie/{truck}")]
        public async Task<TruckSettings> GeefTruckConfiguratie(string truck)
        {
            await using var serviceClient = new TruckWebServiceClient();
            var result = await serviceClient.GeefTruckConfiguratieAsync(truck);
            return result;
        }
    }
}
