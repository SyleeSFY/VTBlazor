using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Client.Core.Pages.PrivateOffice.EducatorOffice
{
    public partial class TaskAdd : ComponentBase
    {
        private TaskEducation newTask = new();
        private List<Discipline> disciplines = new();
        private List<Educator> educators = new();
        private List<Group> groups = new();
        private List<int> selectedGroups = new();
        private List<IBrowserFile> uploadedFiles = new();
        private bool isSubmitting = false;

        protected override async Task OnInitializedAsync()
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            //disciplines = await DisciplineService.GetAllAsync();
            //educators = await EducatorService.GetAllAsync();
            //groups = await GroupService.GetAllAsync();
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
                newTask.Groups = groups.Where(g => selectedGroups.Contains(g.Id)).ToList();
                newTask.CreatedAt = DateTime.UtcNow;

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
            newTask = new TaskEducation();
            selectedGroups.Clear();
            uploadedFiles.Clear();
        }
    }
}
