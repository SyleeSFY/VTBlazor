using Client.Core;
using Client.Core.App;
using Client.Core.App.Services;
using Client.Core.Pages;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Security.Claims;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthStateProvider>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
//todo: засунуть потом это в appsetings
builder.Services.AddScoped(sp => 
    new HttpClient 
    { 
        BaseAddress = new Uri("http://localhost:5129/")
    });
await builder.Build().RunAsync();


public class TokenAuthStateProvider : AuthenticationStateProvider
{
    private ILocalStorageService _localStorage;

    public TokenAuthStateProvider(ILocalStorageService localStorage)
        => _localStorage = localStorage;
    /// <summary>
    /// Метод для создания токена
    /// </summary>
    /// <returns></returns>
    /// 

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {

        try
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
        catch (Exception ex)
        {

            throw;
        }
    }

    private AuthenticationState CreateAnonymousToken()
    {
        var anonymousPrincipal = new ClaimsPrincipal(new ClaimsIdentity());
        return new AuthenticationState(anonymousPrincipal);
    }
}