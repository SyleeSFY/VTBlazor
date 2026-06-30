using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice
{
    public partial class TaskAdd : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

        private Educator _educator;
        private TaskEducationDTO _newTaskDTO;

        private List<Discipline> _disciplines;
        private List<Group> _groups;

        private List<int> _selectedGroups;
        private List<IBrowserFile> _uploadedFiles;

        private int _taskDicipline;
        private string _taskName = string.Empty;
        private string _taskDesc = string.Empty;

        private bool _isError;
        private string _errorMessage;

        private readonly string[] _allowedFileTypes = { ".pdf", ".docx", ".doc", ".xlsx", ".xls", ".pptx", ".ppt", ".txt", ".jpg", ".jpeg", ".png" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10 МБ

        public TaskAdd()
        {
            _newTaskDTO = new TaskEducationDTO();
            _disciplines = new List<Discipline>();
            _groups = new List<Group>();
            _selectedGroups = new List<int>();
            _uploadedFiles = new List<IBrowserFile>();
        }

        protected override async Task OnInitializedAsync()
        {
            _educator = await _apiService.GetEducatorByAuth(await AuthStateTask);
            _disciplines = await _apiService.GetDisciplines();
            _groups = await _apiService.GetGroups();
        }

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
            if (_uploadedFiles.Count == 0 && !string.IsNullOrEmpty(_errorMessage))
                return;

            try
            {
                if (string.IsNullOrEmpty(_taskDesc) && string.IsNullOrEmpty(_taskName))
                    throw new InvalidOperationException("Необходимо заполнить описание или название задания");

                var taskDTO = await FillTask(_newTaskDTO);
                var response = await Http.PostAsJsonAsync($"api/file/PostAddTask", taskDTO);

                Navigation.NavigateTo("/TaskEducator");
            }
            catch (Exception ex)
            {

            }
        }

        private async Task<TaskEducationDTO> FillTask(TaskEducationDTO taskDTO)
        {
            _newTaskDTO.TaskDescription = _taskDesc;
            _newTaskDTO.TaskName = _taskName;
            _newTaskDTO.EducatorId = _educator.Id;
            _newTaskDTO.GroupId = _selectedGroups;
            _newTaskDTO.DiciplineId = _taskDicipline;

            if (_uploadedFiles.Count != 0)
                _newTaskDTO.Files = await AddFiles(_uploadedFiles);

            return taskDTO;
        }

        private async Task<List<TaskFileDTO>> AddFiles(List<IBrowserFile> uploadedFiles)
        {
            _errorMessage = string.Empty;
            var files = new List<TaskFileDTO>();

            foreach (var file in uploadedFiles)
            {
                var fileExtension = Path.GetExtension(file.Name).ToLower();

                if (!_allowedFileTypes.Contains(fileExtension))
                {
                    _isError = true;
                    _errorMessage = $"Файл '{file.Name}' имеет недопустимый формат. Разрешены: {string.Join(", ", _allowedFileTypes)}";
                    continue;
                }

                if (file.Size > MaxFileSize)
                {
                    _isError = true;
                    _errorMessage = $"Файл '{file.Name}' превышает максимальный размер 10 МБ";
                    continue;
                }

                using var stream = file.OpenReadStream(maxAllowedSize: MaxFileSize);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var bytes = memoryStream.ToArray();
                var base64 = Convert.ToBase64String(bytes);

                var taskFileDTO = new TaskFileDTO
                {
                    FileName = file.Name,
                    FileSize = file.Size,
                    ContentBase64 = base64,
                    FileType = fileExtension
                };

                files.Add(taskFileDTO);
            }

            return files;
        }

        private async Task OnFileUpload(InputFileChangeEventArgs e)
        {
            _isError = false;
            _errorMessage = string.Empty;

            var files = e.GetMultipleFiles();
            _uploadedFiles.Clear();

            var errorMessages = new List<string>();

            foreach (var file in files)
            {
                var fileExtension = Path.GetExtension(file.Name).ToLower();

                if (!_allowedFileTypes.Contains(fileExtension))
                {
                    errorMessages.Add($"Файл '{file.Name}' имеет недопустимый формат");
                    continue;
                }

                if (file.Size > MaxFileSize)
                {
                    errorMessages.Add($"Файл '{file.Name}' превышает максимальный размер 10 МБ");
                    continue;
                }

                _uploadedFiles.Add(file);
            }

            if (errorMessages.Any())
            {
                _isError = true;
                _errorMessage = string.Join("<br>", errorMessages);
            }
        }

        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024)
                return $"{bytes} B";
            if (bytes < 1024 * 1024)
                return $"{bytes / 1024} KB";
            return $"{bytes / (1024 * 1024):F1} MB";
        }

        private void ResetForm()
        {
            _newTaskDTO = new TaskEducationDTO();
            _selectedGroups.Clear();
            _uploadedFiles.Clear();
        }
    }
}