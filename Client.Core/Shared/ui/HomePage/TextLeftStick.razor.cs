using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Shared.ui.HomePage
{
    public partial class TextLeftStick
    {
        [Parameter]
        [Required]
        public string Text { get; set; }
        public MarkupString GetMarckupString()
        {
            return new MarkupString(Text);
        }
    }
}

