using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Client.Core.Pages.Public;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice;

public partial class TaskInfo : ComponentBase
{
    [Inject] private IApiService _apiService { get; set; }
    [Parameter] public required int Id { get; set; }

    private TaskEducation _task;

    public TaskInfo()
    {
        _task = new TaskEducation();
    }

    protected override async Task OnInitializedAsync()
    {
        _task = await _apiService.GetTaskEducationById(Id);
        _task.Dicipline = await _apiService.GetDisciplineById(_task.DiciplineId);
    }

    private async Task GetFile(TaskFile entitie)
    {
        var qwe = await _apiService.GetFileFromBD(entitie.Id);
        var tretretre = qwe;
        await DownloadFile(qwe, entitie.FileName);
    }
    
    private async Task DownloadFile(byte[] fileBytes, string fileName)
    {
        // Преобразуем byte[] в base64
        var base64 = Convert.ToBase64String(fileBytes);
        
        // Создаем data URL и инициируем скачивание через JS
        await JS.InvokeVoidAsync("downloadFile", base64, fileName);
    }
}