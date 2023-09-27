using Microsoft.IdentityModel.Tokens;
using SecondCodingExam.Models;
using SecondCodingExam.Services.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecondCodingExam.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtService(
            IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<string> GenerateToken(User User)
        {
            // Generate Keys
            var SecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.JwtKey]));
            // Add Credentials Using Generated Keys
            var Credentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

            // Create User Claims
            var Claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,User.Id.ToString()),
                new Claim(ClaimTypes.Email, User.Email)
            };

            // Generate Token
            var token = new JwtSecurityToken(
                _configuration[Constants.JwtIssuer],
                _configuration[Constants.JwtAudience],
                Claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: Credentials);

            return await Task.FromResult(new JwtSecurityTokenHandler().WriteToken(token));
        }
        public async Task<int> GetUserIdFromToken()
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var Token = _httpContextAccessor.HttpContext.Request.Headers[Constants.Authorization].ToString().Replace(Constants.Bearer, string.Empty);

            TokenHandler.ValidateToken(Token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration[Constants.JwtIssuer],
                ValidAudience = _configuration[Constants.JwtAudience],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration[Constants.JwtKey])),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken ValidatedToken);

            var JwtToken = (JwtSecurityToken)ValidatedToken;
            return await Task.FromResult(int.Parse(JwtToken.Claims.First(x => x.Type == Constants.NameIdentifier).Value));
        }
    }
}