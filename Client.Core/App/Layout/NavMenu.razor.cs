using Microsoft.AspNetCore.Components;

namespace Client.Core.App.Layout;

public partial class NavMenu : ComponentBase
{
    private bool isMenuOpen = false; 
    private void ToggleMenu()
    => isMenuOpen = !isMenuOpen;
    
    private void CloseMenu()
    => isMenuOpen = false;
}