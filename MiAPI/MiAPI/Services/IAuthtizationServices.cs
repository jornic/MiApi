using MiAPI.Models;
using System.Security.Claims;

namespace MiAPI.Services
{
    public interface IAuthotizationServices
    {
        public string GetUserToken(ClaimsPrincipal User);
        public Task<AuthorizationResponse> GetAuthorizationIsValid(MLogin login);
    }
}
