using Microsoft.AspNetCore.Components;
using System.ComponentModel.DataAnnotations;

namespace Client.Core.Features
{
    public partial class ButtonRedirect : ComponentBase
    {
        [Parameter]
        [Required]
        public string Text { get; set; }
        [Parameter]
        [Required]
        public string Redirect { get; set; }
        [Parameter]
        public int Height { get; set; } = 50;
        [Parameter]
        public int Width { get; set; } = 100;
        [Parameter]
        public string MT { get; set; } = "0";

        private async Task OnRedirect()
        {
            Navigation.NavigateTo($"/{Redirect}");
        }
    }
}
