using Client.Core.App.Services;
using Client.Core.Entities.Models.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace Client.Core.App.Services
{
    /// <summary>
    /// Сервис для работы с аутефикацией
    /// </summary>
    public class TokenAuthService : AuthenticationStateProvider
    {
        private ILocalStorageService _localStorage;

        public TokenAuthService(ILocalStorageService localStorage)
            => _localStorage = localStorage;

        /// <summary>
        /// Метод для создания токена
        /// </summary>
        /// <returns></returns>
        /// 
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var tokenUser = await _localStorage.GetAsync<SecurityToken>(nameof(SecurityToken));
            if (tokenUser == null || string.IsNullOrEmpty(tokenUser.AccessToken) || tokenUser.ExpiredAt < DateTime.UtcNow)
                return CreateAnonymousToken();

            var claims = new List<Claim>()
            {
                new Claim (ClaimTypes.Country, "Russia"),
                new Claim (ClaimTypes.Email, tokenUser.Email),
                new Claim (ClaimTypes.Expired, tokenUser.ExpiredAt.ToLongDateString()),
            };

            var idn = new ClaimsIdentity(claims, "Token");
            var principal = new ClaimsPrincipal(idn);
            return new AuthenticationState(principal);
        }

        private AuthenticationState CreateAnonymousToken()
        {
            var anonymousPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymousPrincipal);
        }
    }
}