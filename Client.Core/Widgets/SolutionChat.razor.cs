using Client.Core.Entities.Enums;
using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Client.Core.Widgets 
{
    public partial class SolutionChat : ComponentBase, IDisposable {
        [Inject] private IApiService _apiService { get; set; }
        [Inject] private IJSRuntime _jsRuntime { get; set; }

        [Parameter] public List<MessageInChat> Messages { get; set; }
        [Parameter] public User User { get; set; }
        [Parameter] public StudentSolution Solution { get; set; }

        private readonly string[] _allowedFileTypes = { ".pdf", ".docx", ".doc", ".xlsx", ".xls", ".pptx", ".ppt", ".txt", ".jpg", ".jpeg", ".png" };
        private const long MaxFileSize = 10 * 1024 * 1024; // 10 МБ

        private string _newMessage = "";
        private ElementReference _messagesContainer;
        private IJSObjectReference? _module;
        private List<IBrowserFile> _uploadedFiles;
        private System.Timers.Timer _timer;
        private bool _isLoading = false;
        private bool _isDisposed = false;
        private bool _shouldScrollToBottom = false;

        private string _errorMessage = string.Empty;

        public SolutionChat() 
        {
            _uploadedFiles = new List<IBrowserFile>();
        }

        protected override async Task OnInitializedAsync() 
        {
            await LoadMessages();

            _timer = new System.Timers.Timer(10000);
            _timer.Elapsed += OnTimerElapsed;
            _timer.AutoReset = true;
            _timer.Start();
        }

        private async void OnTimerElapsed(object sender, System.Timers.ElapsedEventArgs e) 
        {
            if (_isLoading || _isDisposed) return;

            _isLoading = true;
            try {
                await InvokeAsync(async () => {
                    var oldCount = Messages?.Count ?? 0;
                    await LoadMessages();

                    if (Messages != null && Messages.Count > oldCount) {
                        _shouldScrollToBottom = true; //прокрутка
                        StateHasChanged();
                    }
                });
            }
            catch (Exception ex) {
                Console.WriteLine($"Ошибка при обновлении сообщений: {ex.Message}");
            }
            finally {
                _isLoading = false;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender) 
        {
            if (firstRender) {
                _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Widgets/SolutionChat.razor.js");
                await Task.Delay(300);
                await ScrollToBottom();
            }

            if (_shouldScrollToBottom) {
                await ScrollToBottom();
                _shouldScrollToBottom = false;
            }
        }

        private async Task ScrollToBottom() 
        {
            try {
                if (_module != null) {
                    await _module.InvokeVoidAsync("scrollToBottom", _messagesContainer);
                }
            }
            catch (Exception ex) 
            {
            }
        }

        private string GetSenderName(MessageInChat message) 
        {
            if (message.SenderId == User.Id)
                return "Вы";

            return message.SenderRole switch {
                Role.educator => "Преподаватель",
                Role.admin => "Администратор",
                Role.student => "Студент",
                _ => "Пользователь"
            };
        }

        private async Task GetFile(FileInChat fileId) 
        {
            try {
                var file = await _apiService.GetMessageFileByte(fileId.Id);
                await DownloadFile(file, fileId.FileName);
            }
            catch (Exception ex) 
            {
            }
        }

        private async Task DownloadFile(byte[] fileBytes, string fileName) 
        {
            var base64 = Convert.ToBase64String(fileBytes);
            if (_module != null) {
                await _module.InvokeVoidAsync("downloadFile", base64, fileName);
            }
        }

        private async Task LoadMessages() 
        {
            try {
                if (Solution?.SolutionChat?.Id != null) {
                    var chat = await _apiService.GetChatById(Solution.SolutionChat.Id);
                    if (chat?.Messages != null) {
                        var oldCount = Messages?.Count ?? 0;
                        var oldLastMessageId = Messages?.LastOrDefault()?.Id;

                        Messages = chat.Messages;

                        var newMessages = Messages?.Count > oldCount;
                        var lastMessageChanged = Messages?.LastOrDefault()?.Id != oldLastMessageId;

                        if (newMessages && lastMessageChanged && Messages?.Any() == true) {
                            var lastMessage = Messages.Last();

                            if (lastMessage.SenderId != User.Id) {
                                var participant = chat.Participants?
                                    .FirstOrDefault(p => p.SenderId != User.Id);

                                if (participant != null) {
                                    await _apiService.DeleteParticipant(participant.Id);
                                }
                            }

                            //_shouldScrollToBottom = true;
                        }

                        if (oldCount == 0) {
                            _shouldScrollToBottom = true;
                        }
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

            try {
                var messageDto = new MessageInChatDTO {
                    ChatId = Solution.SolutionChat.Id,
                    SenderId = User.Id,
                    SenderRole = User.Role,
                    MessageText = _newMessage,
                };

                if (_uploadedFiles.Any())
                    messageDto.Files = await AddFiles(_uploadedFiles);

                await _apiService.PostMessage(messageDto);

                _newMessage = "";
                _uploadedFiles.Clear();

                await LoadMessages();
                _shouldScrollToBottom = true;

                StateHasChanged();
            }
            catch (Exception ex) {
                Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
            }
        }

        #region FileWork
        private async Task<List<FileInChatDTO>> AddFiles(List<IBrowserFile> uploadedFiles) {
            var files = new List<FileInChatDTO>();

            foreach (var file in uploadedFiles) {
                using var stream = file.OpenReadStream(maxAllowedSize: MaxFileSize);
                using var memoryStream = new MemoryStream();
                await stream.CopyToAsync(memoryStream);

                var bytes = memoryStream.ToArray();
                var base64 = Convert.ToBase64String(bytes);

                var fileDTO = new FileInChatDTO {
                    FileName = file.Name,
                    FileSize = file.Size,
                    ContentBase64 = base64,
                    FileType = Path.GetExtension(file.Name)
                };

                files.Add(fileDTO);
            }

            return files;
        }

        private async Task OnFileUpload(InputFileChangeEventArgs e) {
            var files = e.GetMultipleFiles();

            _errorMessage = string.Empty;
            _uploadedFiles.Clear();

            foreach (var file in files) {
                if (file.Size > MaxFileSize) {
                    _errorMessage = $"Файл превышает максимальный размер {MaxFileSize / 1024 / 1024} МБ";
                    StateHasChanged();
                    return;
                }

                var fileExtension = Path.GetExtension(file.Name).ToLower();
                if (!_allowedFileTypes.Contains(fileExtension)) {
                    var allowedTypes = string.Join(", ", _allowedFileTypes);
                    _errorMessage = $"Файл имеет неподдерживаемый тип. Разрешены: {allowedTypes}";
                    StateHasChanged();
                    return;
                }
            }

            _uploadedFiles.AddRange(files);
        }

        private string FormatFileSize(long bytes) 
        {
            if (bytes < 1024) return $"{bytes} B";
            if (bytes < 1024 * 1024) return $"{bytes / 1024} KB";
            return $"{bytes / (1024 * 1024):F1} MB";
        }

        private void ClearFiles() => _uploadedFiles.Clear();
        private void RemoveFile(IBrowserFile fileToRemove) => _uploadedFiles.Remove(fileToRemove);
        #endregion

        public void Dispose() 
        {
            if (!_isDisposed) {
                _isDisposed = true;
                _timer?.Stop();
                _timer?.Dispose();
            }
        }
    }
}