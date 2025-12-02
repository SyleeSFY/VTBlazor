using Client.Core.Shared.Models;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Features.EducatorsPage;

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
    
    private void GoToEducatorCard()
        => Navigation.NavigateTo($"/EducatorCard/{EducatorEntitie.Id}");
}