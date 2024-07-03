using MiAPI.Data.Interfaz;
using MiAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MiAPI.Services
{
    public class AuthorizationServices:IAuthotizationServices
    {
        private IConfiguration _configuration;
        private ILogInUsers _LogIn;

        public AuthorizationServices(IConfiguration configuration,ILogInUsers logInUsers)
        {
            _configuration = configuration;
            _LogIn = logInUsers;
        }

        public async Task<AuthorizationResponse> GetAuthorizationIsValid(MLogin login)
        {
            AuthorizationResponse response = null;
            if (await _LogIn.VerificarUsuario(login) && login != null)
            {
                response = new AuthorizationResponse
                {
                    token = GenerateJwtToken(login.User),
                    state = true
                };
            }
            return response;
        }

        public string GetUserToken(ClaimsPrincipal User)=>
             User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;

        private string GenerateJwtToken(string username)
        {
            string _key = _configuration.GetValue<string>("JwtKey");
            byte[] key = Encoding.ASCII.GetBytes(_key);
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
