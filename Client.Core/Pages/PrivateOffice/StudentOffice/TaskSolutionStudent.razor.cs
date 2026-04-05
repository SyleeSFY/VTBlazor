using Client.Core.Entities.Enums;
using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.StudentOffice
{
    public partial class TaskSolutionStudent : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }
        [Parameter] public required int Id { get; set; }

        private List<IBrowserFile> _uploadedFiles;

        private IJSObjectReference? _module;
        private TaskEducation _task;
        private Student _student;

        private SolutionStudentDTO _taskSolution;

        string _solutionText = string.Empty;

        public TaskSolutionStudent()
        {
            _task = new TaskEducation();
            _taskSolution = new SolutionStudentDTO();
            _uploadedFiles = new List<IBrowserFile>();
        }

        protected override async Task OnInitializedAsync()
        {
            _student = await _apiService.GetStudentByAuth(await AuthStateTask);


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

        private void ClearFiles()
   => _uploadedFiles.Clear();

        private void RemoveFile(IBrowserFile fileToRemove)
            => _uploadedFiles.Remove(fileToRemove);

        private async Task HandleValidSubmit()
        {
            try
            {
                if (string.IsNullOrEmpty(_solutionText))
                    throw new InvalidOperationException("Необходимо заполнить описание");
                var solutionDTO = await FillSolution(_taskSolution);
                
                var response = await _apiService.PostSolutionStudent(solutionDTO);

                Navigation.NavigateTo("/TaskEducator");
            }
            catch (Exception ex)
            {


            }
        }

        private async Task<SolutionStudentDTO> FillSolution(SolutionStudentDTO taskSolution)
        {
            taskSolution.TaskId = _task.Id;
            taskSolution.StudentId = _student.Id;
            taskSolution.SolutionText = _solutionText;
            taskSolution.CreatedAt = DateTime.Now;
            taskSolution.UpdatedAt = DateTime.Now;

            if (_uploadedFiles.Count != 0)
                taskSolution.SolutionFiles = await AddFiles(_uploadedFiles);

            return taskSolution;
        }

        private async Task<List<SolutionFileDTO>> AddFiles(List<IBrowserFile> uploadedFiles)
        {
            var files = new List<SolutionFileDTO>();

            foreach (var file in uploadedFiles)
            {
                using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var bytes = memoryStream.ToArray();
                var base64 = Convert.ToBase64String(bytes);

                var taskFileDTO = new SolutionFileDTO
                {
                    FileName = file.Name,
                    OriginalFileName = file.Name,
                    FileSize = file.Size,
                    ContentBase64 = base64,
                    FileType = Path.GetExtension(file.Name),
                    UploadedAt = DateTime.Now,
                };

                files.Add(taskFileDTO);
            }

            return files;
        }
    }
}