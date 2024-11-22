using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Rihappy.HealthCheck.Application.DTOs.Request;
using Rihappy.HealthCheck.Application.Service;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Rihappy.HealthCeck.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string ValidUsername = "admin";
        private const string ValidPassword = "1234";
        private const string SecretKey = "my_super_secure_and_longer_secret_key_123!";

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDTO request)
        {
            if (request.Username == ValidUsername && request.Password == ValidPassword)
            {
                var token = GenerateJwtToken(request.Username);
                return Ok(new { success = true, token });
            }

            return Unauthorized(new { success = false, message = "Usuário ou senha inválidos!" });
        }

        private string GenerateJwtToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "AuthAPI",
                audience: "AuthAPI",
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

