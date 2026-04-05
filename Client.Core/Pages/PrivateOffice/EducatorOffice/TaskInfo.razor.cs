using Client.Core.Entities.Enums;
using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Client.Core.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice;

public partial class TaskInfo : ComponentBase
{
    [Inject] private IApiService _apiService { get; set; }
    [Parameter] public required int Id { get; set; }

    private IJSObjectReference? _module;

    private TaskEducation _task;
    private List<StudentSolution> _solutions;
    private List<User> _usersByGroup;

    public TaskInfo()
    {
        _task = new TaskEducation();
        _solutions = new List<StudentSolution>();
        _usersByGroup = new List<User>();
    }

    protected override async Task OnInitializedAsync()
    {
        _task = await _apiService.GetTaskEducationById(Id);
        _task.Dicipline = await _apiService.GetDisciplineById(_task.DiciplineId);
        _solutions = await _apiService.GetSolutionByTaskIdSimple(_task.Id);

        foreach (var group in _task.Groups)
        {
            _usersByGroup.AddRange(await _apiService.GetUsersStudentByGroup(group.Id));
        }
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

    private string GetStudentFullName(int studentId)
    {
        var user = GetUserByStudentId(studentId);
        return user != null ? $"{user.LastName} {user.FirstName} {user.MiddleName}".Trim() : "Неизвестный студент";
    }

    private string GetStudentGroupName(int studentId)
    {
        var user = GetUserByStudentId(studentId);
        return user?.Student?.Group?.Name ?? "-";
    }

    private User GetUserByStudentId(int studentId)
        => _usersByGroup.FirstOrDefault(u => u.Student.Id == studentId);

    private string GetSolutionStatus(SolutionStatus status)
        => GlobalData.GetSolutionStatus[status];

    private async Task GoTaskInfoDesc(int taskId)
    {
        Navigation.NavigateTo($"/TaskInfoDesc/{taskId}");
    }
}