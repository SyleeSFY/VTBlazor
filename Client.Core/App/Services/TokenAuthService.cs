using Client.Core.App.Services;
using Client.Core.Entities.Models.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Client.Core.App.Services
{
    public class TokenAuthService : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorage;

        public TokenAuthService(ILocalStorageService localStorage)
            => _localStorage = localStorage;

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenUser = await _localStorage.GetAsync<Cookie>("VT");

            if (tokenUser == null || string.IsNullOrEmpty(tokenUser.JWT))
                return CreateAnonymousToken();

            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(tokenUser.JWT);
            var claims = jwtToken.Claims.ToList();

            // Нормализация роли: все варианты role приводим к ClaimTypes.Role
            var roleClaims = claims
                .Where(c => c.Type == ClaimTypes.Role ||
                            c.Type == "role" ||
                            c.Type == "Role")
                .Select(c => new Claim(ClaimTypes.Role, c.Value))
                .ToList();

            if (roleClaims.Any())
            {
                claims.RemoveAll(c => c.Type == ClaimTypes.Role ||
                                      c.Type == "role" ||
                                      c.Type == "Role");
                claims.AddRange(roleClaims);
            }

            var identity = new ClaimsIdentity(claims, "Token");
            var principal = new ClaimsPrincipal(identity);

            return new AuthenticationState(principal);
        }

        private AuthenticationState CreateAnonymousToken()
        {
            var anonymousPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymousPrincipal);
        }
    }
}