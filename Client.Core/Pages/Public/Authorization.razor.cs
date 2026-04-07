using Client.Core.App.Services;
using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.Authentication;
using Client.Core.Shared;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Net.Mail;

namespace Client.Core.Pages.Public
{
    public partial class Authorization : ComponentBase
    {
        [Inject]
        public ILocalStorageService LocalStorageService { get; set; }

        private AuthorizationData _authorization;
        private bool _isError = false;
        private string _errorMessage = string.Empty;

        public Authorization() 
            => _authorization = new AuthorizationData();
        
        protected async Task LoginAsync()
        {
            await Validation();

            if (!_isError)
            {
                var authData = new AuthData()
                {
                    Password = _authorization.Password,
                    Email = _authorization.Email.ToLower()
                };

                var response = await Http.PostAsJsonAsync($"api/Auth/PostAuthUser", authData);
                var result = await response.Content.ReadFromJsonAsync<AuthResponce>();

                if (result != null && result.Success)
                {
                    var token = new Cookie()
                    {
                        Email = _authorization.Email.ToLower(),
                        JWT = result.JwtToken,
                        ExpiredAt = DateTime.Now.AddDays(1)
                    };
                    await LocalStorageService.SetAsync("VT", token);
                    Navigation.NavigateTo($"/profile", true);
                }
                else
                {
                    _errorMessage = GlobalData.ValidError[ValidErrorAuth.InvalidCredentials];
                    _isError = true;
                }
            }
            
        }

        private async Task Validation() 
        {
            _isError = false;
            _errorMessage = string.Empty;

            if (string.IsNullOrWhiteSpace(_authorization.Email))
            {
                _errorMessage = GlobalData.ValidError[ValidErrorAuth.EmailRequired];
                _isError = true;
                return;
            }

            if (string.IsNullOrWhiteSpace(_authorization.Password))
            {
                _errorMessage = GlobalData.ValidError[ValidErrorAuth.PasswordRequired];
                _isError = true;
                return;
            }

            if (_authorization.Password.Length > 50)
            {
                _errorMessage = GlobalData.ValidError[ValidErrorAuth.PasswordTooLong];
                _isError = true;
                return;
            }

            if (!IsValidEmail(_authorization.Email))
            {
                _errorMessage = GlobalData.ValidError[ValidErrorAuth.InvalidEmail];
                _isError = true;
                return ;
            }

            if (_authorization.Password.Length < 6)
            {
                _errorMessage = GlobalData.ValidError[ValidErrorAuth.PasswordTooShort];
                _isError = true;
                return ;
            }
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
                return mailAddress.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}