using Client.Core.App.Services;
using Client.Core.Entities.Models.Authentication;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.Public
{
    public partial class Authorization : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        private AuthorizationData _authorization;
        
        public Authorization() {
            _authorization = new AuthorizationData();
        }

        protected async Task LoginAsync()
        {
            var token = new SecurityToken()
            {
                AccessToken = _authorization.Password,
                Email = _authorization.Email,
                ExpiredAt = DateTime.UtcNow.AddDays(1)
            };
            await LocalStorageService.SetAsync(nameof(SecurityToken), token);
        }
    }
}