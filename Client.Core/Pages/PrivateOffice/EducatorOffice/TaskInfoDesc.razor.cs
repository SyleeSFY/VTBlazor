using Client.Core.Entities.Enums;
using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Client.Core.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections;
using System.Reflection;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice;

public partial class TaskInfoDesc : ComponentBase
{
    [Inject] private IApiService _apiService { get; set; }
    [Parameter] public required int Id { get; set; }

    private IJSObjectReference? _module;

    private StudentSolution _solution;
    private Student _student;
    private User _userStudent;

    public TaskInfoDesc()
    {
        _solution = new StudentSolution();
        _userStudent = new User();
        _student = new Student();
    }

    protected override async Task OnInitializedAsync()
    {
        _solution = await _apiService.GetSolutionById(Id);
        _student = await _apiService.GetStudentById(_solution.StudentId);
        _userStudent = await _apiService.GetUserByUserId(_student.UserId);
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            _module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/PrivateOffice/EducatorOffice/TaskInfo.razor.js");
    }

    private async Task GetFile(SolutionFile fileId)
    {
        var file = await _apiService.GetSolutionFileByte(fileId.Id);
        await DownloadFile(file, fileId.FileName);
    }

    private async Task DownloadFile(byte[] fileBytes, string fileName)
    {
        var base64 = Convert.ToBase64String(fileBytes);
        await _module.InvokeVoidAsync("downloadFile", base64, fileName);
    }

    private string GetSolutionStatus(SolutionStatus status)
        => GlobalData.GetSolutionStatus[status];

    private string GetSolutionColor(SolutionStatus status)
    {
        switch (status)
        {
            case SolutionStatus.Approved: return "green";
            case SolutionStatus.Rejected: return "red";
            default: return string.Empty;
        }
    }

    private async Task UpdateStatus(SolutionStatus status)
    {
        var taskSolution = new SolutionStudentDTO()
        {
            TaskId = _solution.TaskId,
            StudentId = _solution.StudentId,
            SolutionText = _solution.SolutionText,
            CreatedAt = _solution.CreatedAt,
            UpdatedAt = DateTime.Now,
            Status= status
        };

        var success = await _apiService.UpdateSolutionStatus(_solution.Id,taskSolution);

        if (success)
        {
            _solution.Status = taskSolution.Status;
            _solution.UpdatedAt = DateTime.Now;
        }
    }
}