using Client.Core.App.Services;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Features.EducatorInfo;

public partial class EducatorCard : ComponentBase
{
    [Parameter]
    public required Educator EducatorEntitie { get; set; }

    private string GetIMG() {
        var imageData = EducatorEntitie?.EducatorAdditionalInfo?.Image;
        if (!string.IsNullOrEmpty(imageData))
            return $"data:image/jpeg;base64,{imageData}";
        return String.Empty;
    }

    private void GoToEducatorCard() {
        EducatorStateService.SetEducator(EducatorEntitie);
        Navigation.NavigateTo($"/educatorcard/{EducatorEntitie.Id}");
    }
}