using System.Net.Http.Json;
using Client.Core.Shared.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Core.Features.EducatorInfo;

public partial class EducatorCardAddInfo : ComponentBase
{
    [Parameter]
    public required int Id { get; set; }
    
    private Educator? educator;
    private bool isLoading = true;
    private string? errorMessage;
    
    protected override async Task OnInitializedAsync()
    {
        await LoadEducator();
    }
    
    private async Task LoadEducator()
    {
        isLoading = true;
        try
        {
            var json = await JSRuntime.InvokeAsync<string>("sessionStorage.getItem", $"educator_{Id}");
        
            if (!string.IsNullOrEmpty(json))
            {
                educator = System.Text.Json.JsonSerializer.Deserialize<Educator>(json);
                await JSRuntime.InvokeVoidAsync("sessionStorage.removeItem", 
                    $"educator_{Id}");
            }
            else
            {
                educator = await Http.GetFromJsonAsync<Educator>($"api/educators/{Id}");
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }
        finally
        {
            isLoading = false;
        }
    }
    
    private string GetIMG()
    {
        var imageData = educator?.EducatorAdditionalInfo?.Image;
        if (!string.IsNullOrEmpty(imageData))
            return $"data:image/jpeg;base64,{imageData}";
        return String.Empty;
    }
}