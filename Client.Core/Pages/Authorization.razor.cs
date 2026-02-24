using Client.Core.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.ComponentModel.DataAnnotations;

namespace Client.Core.Pages
{
    public partial class Authorization : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }
        private AuthorizationModel _authorization;
        
        public Authorization() {
            _authorization = new AuthorizationModel();
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

    public class AuthorizationModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "Long")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class SecurityToken
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}