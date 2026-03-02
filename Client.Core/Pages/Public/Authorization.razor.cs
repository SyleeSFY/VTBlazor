using Client.Core.App.Services;
using Client.Core.Entities.Models.Authentication;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Net.Http.Json;

namespace Client.Core.Pages.Public
{
    public partial class Authorization : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        private AuthorizationData _authorization;
        private bool _isState = false;

        public Authorization() 
            => _authorization = new AuthorizationData();
        
        protected async Task LoginAsync()
        {
            _isState = false;

            var authData = new AuthData() { 
                Password = _authorization.Password, 
                Email = _authorization.Email
            };

            var response = await Http.PostAsJsonAsync($"api/Auth/PostAuthUser", authData);
            var result = await response.Content.ReadFromJsonAsync<AuthResponce>();

            if (result != null && result.Success) {
                var token = new Cookie() {
                    Email = _authorization.Email, 
                    JWT = result.JwtToken, 
                    ExpiredAt = DateTime.Now.AddDays(1) 
                };
                await LocalStorageService.SetAsync("VT", token);
            }
            else
                _isState = !_isState;
        }
    }
}