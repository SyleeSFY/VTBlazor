using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.App.Layout.Nav;

public partial class NavMenu : ComponentBase
{
    [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

    private bool isMenuOpen = false;
    private bool isAuth = false;
    private void ToggleMenu()
    => isMenuOpen = !isMenuOpen;

    private void CloseMenu()
    => isMenuOpen = false;

    protected override async Task OnInitializedAsync()
    {
        if (AuthStateTask is not null)
        {
            var authState = await AuthStateTask;
            isAuth = authState.User.Identity?.IsAuthenticated ?? false;
        }
    }
}