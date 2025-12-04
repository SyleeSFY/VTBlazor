using System.ComponentModel.DataAnnotations;
using Client.Core.Shared.ui.Model;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Shared.ui.HomePage
{
    public partial class TittleUnderStick : TittleBase
    {
        [Parameter] [Required] public string Tittle { get; set; }
        [Parameter] public int FontSize { get; set; } = 48;
    }
}
