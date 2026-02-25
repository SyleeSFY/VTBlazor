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
builder.Services.AddScoped<AuthenticationStateProvider, TokenAuthService>();
builder.Services.AddScoped<ILocalStorageService, LocalStorageService>();
//todo: засунуть потом это в appsetings
builder.Services.AddScoped(sp => 
    new HttpClient 
    { 
        BaseAddress = new Uri("http://localhost:5129/")
    });
await builder.Build().RunAsync();