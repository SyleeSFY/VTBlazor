using Client.Core.Shared.Enums.HomePage;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Shared.ui.Model;

public abstract class TittleBase : ComponentBase
{
    [Parameter] public JustifyContentEnum JustifyContent { get; set; } = JustifyContentEnum.FlexStart;
        
    public string GetJustifyContent() 
        => JustifyContent.ToCssValue();
}
