using System.Text.Json;
using Client.Core.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.JSInterop;

namespace Client.Core.Features.EducatorInfo;

public partial class EducatorCard : ComponentBase
{
    [Parameter] public required Educator EducatorEntitie { get; set; }

    private string GetIMG()
    {
        var imageData = EducatorEntitie?.EducatorAdditionalInfo?.Image;
        if (!string.IsNullOrEmpty(imageData))
            return $"data:image/jpeg;base64,{imageData}";
        return String.Empty;
    }

    private async Task GoToEducatorCard()
    {
        await JSRuntime.InvokeVoidAsync("sessionStorage.setItem", $"educator_{EducatorEntitie.Id}", JsonSerializer.Serialize(EducatorEntitie));
        
        Navigation.NavigateTo($"/educatorcard/{EducatorEntitie.Id}");
    }
}