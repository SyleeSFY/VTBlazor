using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Client.Core.Widgets
{
    public partial class SolutionChat : ComponentBase, IDisposable
    {
        [Inject] private IApiService _apiService { get; set; }

        [Parameter] public List<MessageInChat> Messages { get; set; }
        [Parameter] public User User { get; set; }
        [Parameter] public StudentSolution Solution { get; set; }

        private string _newMessage = "";
        private ElementReference _fileInput;
        private IJSObjectReference? _module;
        private List<IBrowserFile> _uploadedFiles;
        private System.Timers.Timer _timer;
        private bool _isLoading = false;
        private bool _isDisposed = false;

        public SolutionChat()
        {
            _uploadedFiles = new List<IBrowserFile>();
        }

        protected override async Task OnInitializedAsync()
        {
            await LoadMessages();

            _timer = new System.Timers.Timer(30000); // 30 секунд
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private async void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (_isLoading || _isDisposed) return;

            _isLoading = true;
            try
            {
                await InvokeAsync(async () =>
                {
                    await LoadMessages();
                    StateHasChanged();
                });
            }
            catch (Exception ex)
            {
            }
            finally
            {
                _isLoading = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await JS.InvokeAsync<IJSObjectReference>("import", "./Widgets/SolutionChat.razor.js");
            }
        }

        private async Task GetFile(FileInChat fileId)
        {
            var file = await _apiService.GetMessageFileByte(fileId.Id);
            await DownloadFile(file, fileId.FileName);
        }

        private async Task DownloadFile(byte[] fileBytes, string fileName)
        {
            var base64 = Convert.ToBase64String(fileBytes);
            await _module.InvokeVoidAsync("downloadFile", base64, fileName);
        }

        private async Task LoadMessages()
        {
            try
            {
                if (Solution?.SolutionChat?.Id != null)
                {
                    var chat = await _apiService.GetChatById(Solution.SolutionChat.Id);
                    if (chat?.Messages != null && chat.Messages.Count != Messages?.Count)
                    {
                        Messages = chat.Messages;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(_newMessage) && !_uploadedFiles.Any())
                return;

            try
            {
                var messageDto = new MessageInChatDTO
                {
                    ChatId = Solution.SolutionChat.Id,
                    SenderId = User.Id,
                    SenderRole = User.Role,
                    MessageText = _newMessage,
                };

                if (_uploadedFiles.Any())
                    messageDto.Files = await AddFiles(_uploadedFiles);

                var newMessage = await _apiService.PostMessage(messageDto);

                var newChat = await _apiService.GetChatById(Solution.SolutionChat.Id);
                Messages = newChat.Messages;
                _newMessage = "";
                _uploadedFiles.Clear();

                StateHasChanged();
            }
            catch (Exception ex)
            {
            }
        }

        #region FileWork
        private async Task<List<FileInChatDTO>> AddFiles(List<IBrowserFile> uploadedFiles)
        {
            var files = new List<FileInChatDTO>();

            foreach (var file in uploadedFiles)
            {
                using var stream = file.OpenReadStream(maxAllowedSize: 10 * 1024 * 1024);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var bytes = memoryStream.ToArray();
                var base64 = Convert.ToBase64String(bytes);

                var fileDTO = new FileInChatDTO
                {
                    FileName = file.Name,
                    FileSize = file.Size,
                    ContentBase64 = base64,
                    FileType = Path.GetExtension(file.Name)
                };

                files.Add(fileDTO);
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

        private void ClearFiles()
            => _uploadedFiles.Clear();

        private void RemoveFile(IBrowserFile fileToRemove)
            => _uploadedFiles.Remove(fileToRemove);
        #endregion

        // Реализация IDisposable для очистки таймера
        public void Dispose()
        {
            if (!_isDisposed)
            {
                _isDisposed = true;
                _timer?.Stop();
                _timer?.Dispose();
                Console.WriteLine("Таймер остановлен и очищен");
            }
        }
    }
}