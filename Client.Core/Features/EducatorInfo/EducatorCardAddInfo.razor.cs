using Client.Core.App.Services;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Features.EducatorInfo;

public partial class EducatorCardAddInfo : ComponentBase
{
    [Parameter]
    public required int Id { get; set; }

    private Educator? _educator;
    private bool isLoading = true;
    
    protected override async Task OnInitializedAsync()
    {
        _educator = (EducatorStateService.CurrentEducator?.Id != Id) ? await Http.GetFromJsonAsync<Educator>($"api/educators/{Id}") : EducatorStateService.CurrentEducator;
        isLoading = false;
    }

    private string GetIndentedText(string? text)
    {
        if (string.IsNullOrEmpty(text))
            return string.Empty;

        var paragraphs = text.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        var indentedParagraphs = paragraphs
            .Select(p => "&nbsp;&nbsp;&nbsp;&nbsp;" + p.Trim())
            .ToArray();

        return string.Join("<br/>", indentedParagraphs);
    }

    private string GetIMG()
    {
        var imageData = _educator?.EducatorAdditionalInfo?.Image;
        if (!string.IsNullOrEmpty(imageData))
            return $"data:image/jpeg;base64,{imageData}";
        return String.Empty;
    }
}