using Client.Core.App.Services;
using Client.Core.Entities.Models.Authentication;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Pages.Public;
using Microsoft.AspNetCore.Components.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using static System.Net.WebRequestMethods;

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
            var tokenUser = await _localStorage.GetAsync<Cookie>("VT");
            if (tokenUser == null /*|| string.IsNullOrEmpty(tokenUser.AccessToken)*/ /*|| tokenUser.ExpiredAt < DateTime.UtcNow*/)
                return CreateAnonymousToken();

            var handler = new JwtSecurityTokenHandler();

            var jwtToken = handler.ReadJwtToken(tokenUser.JWT);
            var claims = jwtToken.Claims.ToList();


            var idn = new ClaimsIdentity(claims, "Token");
            var principal = new ClaimsPrincipal(idn);
            return new AuthenticationState(principal);
        }
        //        if (jwtToken.ValidTo<DateTime.UtcNow)
        //{
        //    await _localStorage.RemoveAsync("jwt_token");
        //    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        //}
        


        //// Добавляем тип аутентификации
        //var identity = new ClaimsIdentity(claims, "Jwt");
        //var principal = new ClaimsPrincipal(identity);
        
        //return new AuthenticationState(principal);
        private AuthenticationState CreateAnonymousToken()
        {
            var anonymousPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
            return new AuthenticationState(anonymousPrincipal);
        }
    }
}