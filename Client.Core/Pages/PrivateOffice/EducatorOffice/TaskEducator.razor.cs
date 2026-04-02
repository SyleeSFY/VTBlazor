using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice;

public partial class TaskEducator : ComponentBase
{
    [Inject] private IApiService _apiService { get; set; }
    [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

    private Educator _educator;

    private List<TaskEducation> _taskEducator;
    private List<Discipline> _diciplines;

    public TaskEducator()
    {
        _taskEducator = new List<TaskEducation>();
        _diciplines = new List<Discipline>();
    }

    protected override async Task OnInitializedAsync()
    {
        _educator = await _apiService.GetEducatorByAuth(await AuthStateTask);
        _diciplines = await _apiService.GetDisciplines();
        _taskEducator = await _apiService.GetTasksEducatorByIdSimple(_educator.Id);
    }

    private async Task GoTaskInfo(int taskId)
    {
        Navigation.NavigateTo($"/TaskInfo/{taskId}");
    }
}