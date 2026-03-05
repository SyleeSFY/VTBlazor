using Microsoft.AspNetCore.Components;

namespace Client.Core.App.Layout
{
    public partial class FooterMenu : ComponentBase
    {
        [Parameter] public string ColorBG { get; set; } = "235f26";
    }
}
