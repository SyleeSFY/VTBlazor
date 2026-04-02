using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Pages.PrivateOffice.StudentOffice
{
    public partial class TaskStudentTable : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

        private Student _student;

        private List<TaskEducation> _taskEducator;
        private List<Discipline> _diciplines;

        public TaskStudentTable()
        {
            _taskEducator = new List<TaskEducation>();
            _diciplines = new List<Discipline>();
        }

        protected override async Task OnInitializedAsync()
        {
            _student = await _apiService.GetStudentByAuth(await AuthStateTask);
            _diciplines = await _apiService.GetDisciplines();
            _taskEducator = await _apiService.GetTasksEducatorByGroup(_student.GroupId);
        }

        private async Task GoTaskInfo(int taskId)
        {
            Navigation.NavigateTo($"/TaskInfo/{taskId}");
        }
    }
}
