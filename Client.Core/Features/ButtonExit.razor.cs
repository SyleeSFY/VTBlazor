using Client.Core.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Client.Core.Features
{
    public partial class ButtonExit : ComponentBase
    {
        private ILocalStorageService _localStorage;

        public ButtonExit(ILocalStorageService localStorage)
            => _localStorage = localStorage;

        private async Task OnExit()
        {
            await _localStorage.RemoveAsync("VT");
            Navigation.NavigateTo($"/", true);
        }
    }
}
