using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using System.Security.Claims;
  
namespace Client.Core.Pages.PrivateOffice.EducatorOffice
{
    public partial class TaskAdd : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

        private Educator _educator;
        private TaskEducation _newTask;

        private List<Discipline> _disciplines;
        private List<Group> _groups;

        private List<int> _selectedGroups;
        private List<IBrowserFile> _uploadedFiles;

        private int _taskDicipline;
        private string _taskName = string.Empty;
        private string _taskDesc = string.Empty;

        public TaskAdd()
        {
            _newTask = new TaskEducation();
            _disciplines = new List<Discipline>();
            _groups = new List<Group>();
            _selectedGroups = new List<int>();
            _uploadedFiles = new List<IBrowserFile>();
        }

        protected override async Task OnInitializedAsync()
            => await LoadData();

        private void ClearFiles()
           => _uploadedFiles.Clear();

        private void RemoveFile(IBrowserFile fileToRemove)
            => _uploadedFiles.Remove(fileToRemove);

        private void SelectGroup(int groupId, bool isChecked)
        {
            if (isChecked)
            {
                if (!_selectedGroups.Contains(groupId))
                    _selectedGroups.Add(groupId);
            }
            else
                _selectedGroups.Remove(groupId);
        }

        private async Task HandleValidSubmit()
        {
            try
            {
                if(string.IsNullOrEmpty(_taskDesc) && string.IsNullOrEmpty(_taskName))
                    throw new InvalidOperationException("Необходимо заполнить описание или название задания");

                _newTask.TaskDescription = _taskDesc;
                _newTask.TaskName = _taskName;
                _newTask.EducatorId = _educator.Id;
                _newTask.Groups = _groups.Where(g => _selectedGroups.Contains(g.Id)).ToList();
                _newTask.CreatedAt = DateTime.UtcNow;

                // Сохраняем задание
                //var createdTask = await TaskService.CreateAsync(newTask);

                //// Загружаем файлы, если они есть
                //if (uploadedFiles.Any())
                //{
                //    await TaskService.UploadFilesAsync(createdTask.Id, uploadedFiles);
                //}

                Navigation.NavigateTo("/TaskTable");
            }
            catch (Exception ex)
            {
                // Обработка ошибок
                Console.WriteLine($"Ошибка при сохранении: {ex.Message}");
            }
        }

        private async Task OnFileUpload(InputFileChangeEventArgs e)
        {
            var files = e.GetMultipleFiles();
            _uploadedFiles.Clear();
            _uploadedFiles.AddRange(files);
        }

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes} B";
            if (bytes < 1024 * 1024)
                return $"{bytes / 1024} KB";
            return $"{bytes / (1024 * 1024):F1} MB";
        }

        private async Task LoadData()
        {
            await GetEducator();
            _disciplines = await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines");
            _groups = await Http.GetFromJsonAsync<List<Group>>("api/educators/GetGroups");
        }

        private async Task GetEducator()
        {
            var authState = await AuthStateTask;
            var user = authState.User;

            if (!user.Identity?.IsAuthenticated ?? false)
                return;

            var userIdString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdString, out int userId))
                _educator = await Http.GetFromJsonAsync<Educator>($"api/educators/GetEducatorSimple/{userId}");
        }

        private void ResetForm()
        {
            _newTask = new TaskEducation();
            _selectedGroups.Clear();
            _uploadedFiles.Clear();
        }
    }
}