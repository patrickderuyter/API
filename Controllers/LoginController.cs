using Microsoft.AspNetCore.Mvc;

namespace LESAPI.Controllers
{
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
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
                var secretKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(ConfigurationManager.AppSetting["JWT:Secret"] ?? string.Empty));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var tokeOptions = new JwtSecurityToken(
                    issuer: ConfigurationManager.AppSetting["JWT:ValidIssuer"],
                    audience: ConfigurationManager.AppSetting["JWT:ValidAudience"],
                    claims: new List<Claim>(),
                    expires: DateTime.Now.AddMinutes(6),
                    signingCredentials: signinCredentials
                );


                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return new JWTTokenResponse { Token = tokenString, Gebruiker = result.Gebruiker };
            }

            return new JWTTokenResponse { Token = "", Gebruiker = result.Gebruiker, Status = result.Status};
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
