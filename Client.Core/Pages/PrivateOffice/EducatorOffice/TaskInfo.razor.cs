using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice;

public partial class TaskInfo : ComponentBase
{
    [Inject] private IApiService _apiService { get; set; }
    [Parameter] public required int Id { get; set; }

    private IJSObjectReference? _module;

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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/PrivateOffice/EducatorOffice/TaskInfo.razor.js");
    }

    private async Task GetFile(TaskFile entitie)
    {
        var file = await _apiService.GetFileByte(entitie.Id);
        await DownloadFile(file, entitie.FileName);
    }
    
    private async Task DownloadFile(byte[] fileBytes, string fileName)
    {
        var base64 = Convert.ToBase64String(fileBytes);
        await _module.InvokeVoidAsync("downloadFile", base64, fileName);
    }
}