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
        private readonly ILogger<LoginController> logger;

        public LoginController(ILogger<LoginController> logger)
        {
            this.logger = logger;
        }

        [HttpPost("Inloggen/{truckNumber}/{pin}")]
        public async Task<JWTTokenResponse> Inloggen(string truckNumber, string pin)
        {
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.InloggenAsync(truckNumber, pin);
                if (result.Status == LoginStatus.Geslaagd)
                {
                    return CreateToken(result.Gebruiker);
                }
                logger.LogError($"Token created for {truckNumber}");
                return new JWTTokenResponse
                { Token = "", Gebruiker = result.Gebruiker, Status = result.Status, RefreshToken = "" };
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new JWTTokenResponse();//return empty token.
            }
        }

        private static JWTTokenResponse CreateToken(TruckGebruiker gebruiker)
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

            var refreshToken = GenerateRefreshToken();
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
        public Task<JWTTokenResponse> RefreshToken(JWTTokenResponse originalJwtTokenResponse)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(originalJwtTokenResponse.RefreshToken)
                    && string.IsNullOrWhiteSpace(originalJwtTokenResponse.Token))
                {
                    return Task.FromResult(new JWTTokenResponse { Token = "", Gebruiker = null, Status = LoginStatus.GebruikerOnbekend, RefreshToken = "" });
                }

                return Task.FromResult(CreateToken(originalJwtTokenResponse.Gebruiker));
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return Task.FromResult(new JWTTokenResponse());//return empty token.
            }
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
            try
            {
                await using var serviceClient = new TruckWebServiceClient();
                var result = await serviceClient.GeefTruckConfiguratieAsync(truck);
                return result;
            }
            catch (Exception ex)
            {
                logger.LogError(message: ex.Message);
                return new TruckSettings();//return empty.
            }
        }
    }
}
