using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Client.Core.Pages.Public;
using Microsoft.AspNetCore.Components;

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
}