using System.ComponentModel.DataAnnotations;
using Client.Core.Shared.ui.Model;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Shared.ui.HomePage
{
    public partial class TittleUnderStick : TittleBase
    {
        [Parameter] [Required] public string Tittle { get; set; }
        [Parameter] public bool State { get; set; } = true;
        [Parameter] public int FontSize { get; set; } = 48;
        [Parameter] public int PaddinLeft { get; set; } = 5;
        [Parameter] public string ColorText { get; set; } = "000000";
        [Parameter] public string ColorStick { get; set; } = "009639";

    }
}
