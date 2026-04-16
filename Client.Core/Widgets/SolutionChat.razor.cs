using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace Client.Core.Widgets
{
    public partial class SolutionChat : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }

        [Parameter] public List<MessageInChat> Messages { get; set; }
        [Parameter] public User User { get; set; }
        [Parameter] public StudentSolution Solution { get; set; }

        private string _newMessage = "";
        private List<IBrowserFile> _selectedFiles = new();
        private ElementReference _fileInput;
        private IJSObjectReference? _module;

        protected override async Task OnInitializedAsync()
        {
            await LoadMessages();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await JS.InvokeAsync<IJSObjectReference>("import", "./Pages/PrivateOffice/EducatorOffice/TaskInfo.razor.js");
            }
        }

        private async Task LoadMessages()
        {
            try
            {
                // if (ChatId.HasValue)
                // {
                //     _messages = await ApiService.GetChatMessages(ChatId.Value);
                // }
                // else
                // {
                //     // Если чат еще не создан, создаем его
                //     var chat = await ApiService.CreateChatForSolution(SolutionId);
                //     _messages = chat.Messages;
                // }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка загрузки сообщений: {ex.Message}");
            }
        }

        private async Task HandleFileSelected(InputFileChangeEventArgs e)
        {
            _selectedFiles = e.GetMultipleFiles().ToList();
        }

        private async Task SendMessage()
        {
            if (string.IsNullOrWhiteSpace(_newMessage) && !_selectedFiles.Any())
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

                var newMessage = await _apiService.PostMessage(messageDto);

                // _messages.Add(newMessage);
                // _newMessage = "";
                // _selectedFiles.Clear();

                StateHasChanged();

                // Прокрутка к новому сообщению
                await JS.InvokeVoidAsync("scrollToBottom", "messages-container");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка отправки сообщения: {ex.Message}");
            }
        }

        private async Task DownloadFileAsync(FileInChat file)
        {
            // var fileBytes = await ApiService.GetChatFileBytes(file.Id);
            // var base64 = Convert.ToBase64String(fileBytes);
            // await _module.InvokeVoidAsync("downloadFile", base64, file.FileName);
        }
    }
}
