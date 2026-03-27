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


        private List<Group> groups = new();
        private List<int> selectedGroups = new();
        private List<IBrowserFile> uploadedFiles = new();
        private bool isSubmitting = false;

        public TaskAdd()
        {
            _newTask = new TaskEducation();
        }

        protected override async Task OnInitializedAsync()
            => await LoadData();

        private async Task LoadData()
        {
            await GetEducator();
            _disciplines = await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines");
            //groups = await GroupService.GetAllAsync();
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

        private void ToggleGroup(int groupId, object isChecked)
        {
            if ((bool)isChecked)
            {
                if (!selectedGroups.Contains(groupId))
                    selectedGroups.Add(groupId);
            }
            else
            {
                selectedGroups.Remove(groupId);
            }
        }

        private async Task OnFileUpload(ChangeEventArgs e)
        {
            var files = e.Value as IEnumerable<IBrowserFile>;
            if (files != null)
            {
                uploadedFiles.Clear();
                uploadedFiles.AddRange(files);
            }
        }

        private async Task HandleValidSubmit()
        {
            isSubmitting = true;

            try
            {
                // Устанавливаем выбранные группы
                _newTask.Groups = groups.Where(g => selectedGroups.Contains(g.Id)).ToList();
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
            finally
            {
                isSubmitting = false;
            }
        }

        private void ResetForm()
        {
            _newTask = new TaskEducation();
            selectedGroups.Clear();
            uploadedFiles.Clear();
        }
    }
}
