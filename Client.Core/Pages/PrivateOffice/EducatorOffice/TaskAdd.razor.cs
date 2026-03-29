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
            try
            {
                if(string.IsNullOrEmpty(_taskDesc) && string.IsNullOrEmpty(_taskName))
                    throw new InvalidOperationException("Необходимо заполнить описание или название задания");
                var taskDTO = await FillTask(_newTaskDTO);
                var response = await Http.PostAsJsonAsync($"api/educators/PostAddTask", taskDTO);

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
            _newTaskDTO.GroupId  = _selectedGroups;
            _newTaskDTO.DiciplineId = _taskDicipline; 

            if (_uploadedFiles.Count != 0)
                _newTaskDTO.Files = await AddFiles(_uploadedFiles);
            
            return taskDTO;
        }
            
        private async Task<List<TaskFileDTO>> AddFiles(List<IBrowserFile> uploadedFiles)
        {
            var files = new List<TaskFileDTO>();
            
            foreach (var file in uploadedFiles)
            {
                using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);
        
                var bytes = memoryStream.ToArray();
                var base64 = Convert.ToBase64String(bytes);
        
                var taskFileDTO = new TaskFileDTO
                {
                    FileName = file.Name,
                    FileSize = file.Size,
                    ContentBase64 = base64,
                    FileType = Path.GetExtension(file.Name)
                };
        
                files.Add(taskFileDTO);
            }
    
            return files;
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

        private void ResetForm()
        {
            _newTaskDTO = new TaskEducationDTO();
            _selectedGroups.Clear();
            _uploadedFiles.Clear();
        }
    }
}