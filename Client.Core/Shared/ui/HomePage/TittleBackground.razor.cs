using System.ComponentModel.DataAnnotations;
using Client.Core.Shared.ui.Model;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Shared.ui.HomePage
{
    public partial class TittleBackground : TittleBase
    {
        [Parameter]
        [Required]
        public string Tittle { get; set; } = string.Empty;

        [Parameter] 
        [Required] 
        public string Color { get; set; } = "006928d8";
        [Parameter]
        public int PaddingRight { get; set; } = 40;
        [Parameter]
        public int PaddingLeft { get; set; } = 40;
    }
}
