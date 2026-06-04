using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Client.Core.Pages.PrivateOffice.EducatorOffice;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Pages.PrivateOffice.StudentOffice
{
    public partial class TaskStudentTable : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

        private Student _student;

        private List<StudentSolution> _solutions;
        private List<TaskEducation> _taskEducator;
        private List<Discipline> _diciplines;

        public TaskStudentTable()
        {
            _taskEducator = new List<TaskEducation>();
            _diciplines = new List<Discipline>();
            _solutions = new List<StudentSolution>();
        }

        protected override async Task OnInitializedAsync()
        {
            _student = await _apiService.GetStudentByAuth(await AuthStateTask);
            _diciplines = await _apiService.GetDisciplines();
            _taskEducator = await _apiService.GetTasksEducatorByGroup(_student.GroupId);
            _solutions = await _apiService.GetSolutionsByStudentId(_student.Id);
        }

        private async Task GoTaskInfo(int taskId)
        {
            Navigation.NavigateTo($"/TaskSolutionStudent/{taskId}");
        }

        private bool HasUnreadMessages(TaskEducation task) 
        {

            var solution = _solutions
                .FirstOrDefault(s => s.TaskId == task.Id && s.StudentId == _student?.Id);

            if (solution?.SolutionChat?.Participants == null)
                return false;

            return solution.SolutionChat.Participants
                .Any(p => p.SenderId != _student?.UserId && p.HasUnreadMessages);
        }
    }
}
