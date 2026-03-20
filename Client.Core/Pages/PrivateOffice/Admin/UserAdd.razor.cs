using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class UserAdd : ComponentBase
    {
        private Role _activeRole = Role.student;

        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _middleName = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;

        private string _studentGroup = string.Empty;

        private string _profession = string.Empty;
        private string _academicDegree = string.Empty;

        private string _educationLevel = string.Empty;
        private string _academicTitle = string.Empty;
        private string _specialty = string.Empty;
        private string _qualification = string.Empty;
        private string _additionalInfo = string.Empty;
        private string _imageBase64 = string.Empty;
        private string _imagePreview = string.Empty;

        private List<Discipline> _diciplines = new List<Discipline>();
        private List<int> _selectedDisciplineIds = new List<int>();

        private string _adminPosition = string.Empty;

        private async Task OnImageSelected(ChangeEventArgs e)
        {
            var file = e.Value as Microsoft.AspNetCore.Components.Forms.IBrowserFile;
            if (file != null)
            {
                var buffer = new byte[file.Size];
                await file.OpenReadStream().ReadAsync(buffer);
                _imageBase64 = Convert.ToBase64String(buffer);
                _imagePreview = $"data:{file.ContentType};base64,{_imageBase64}";
            }
        }

        protected override async Task OnInitializedAsync()
            => _diciplines = await GetEducators();

        private async Task<List<Discipline>> GetEducators()
            => await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines");

        private void ToggleDiscipline(int disciplineId, object value)
        {
            if (value is bool isChecked)
            {
                if (isChecked && !_selectedDisciplineIds.Contains(disciplineId))
                    _selectedDisciplineIds.Add(disciplineId);
                else if (!isChecked && _selectedDisciplineIds.Contains(disciplineId))
                    _selectedDisciplineIds.Remove(disciplineId);
            }
        }

        private async Task SaveUser()
        {
            var user = await CreateUser();

            switch (_activeRole)
            {
                case Role.educator:
                    user.Educator = await CreateEducator();
                    break;
                case Role.student:
                    user.Student = await CreateStudent();
                    break;
                case Role.admin:
                    user.Administrator = await CreateAdmin();
                    break;
                default:
                    break;
            }
            var response = await Http.PostAsJsonAsync($"api/User/PostAddUser", user);
            ResetForm();
        }

        private async Task<UserDTO> CreateUser()
            => new UserDTO
            {
                Email = _email,
                PasswordHash = _password,
                FirstName = _firstName,
                LastName = _lastName,
                MiddleName = _middleName,
                Role = _activeRole
            };

        private async Task<StudentDTO> CreateStudent()
            => new StudentDTO
            {
                GroupId = _studentGroup
            };

        private async Task<EducatorDTO> CreateEducator()
            => new EducatorDTO
            {
                Profession = _profession,
                FullName = $"{_lastName} {_firstName} {_middleName}",
                AcademicDegree = _academicDegree,
                EducatorAdditionalInfo = new EducatorAdditionalInfoDTO
                {
                    EducationLevel = _educationLevel,
                    AcademicTitle = _academicTitle,
                    SpecialtyOrFieldOfStudy = _specialty,
                    Qualification = _qualification,
                    AdditionalInfo = _additionalInfo,
                    Image = _imageBase64,
                    EducatorDisciplines = _selectedDisciplineIds.Select(id => new EducatorDisciplineDTO
                    {
                        DisciplineId = id
                    }).ToList()
                }
            };

        private async Task<AdminDTO> CreateAdmin()
            => new AdminDTO
            {
                Position = _adminPosition
            };


        private void SetActiveTable(Role level)
            => _activeRole = level;

        private void ResetForm()
        {
            _lastName = string.Empty;
            _firstName = string.Empty;
            _middleName = string.Empty;
            _password = string.Empty;
            _email = string.Empty;

            _studentGroup = string.Empty;

            _profession = string.Empty;
            _academicDegree = string.Empty;
            _educationLevel = string.Empty;
            _academicTitle = string.Empty;
            _specialty = string.Empty;
            _qualification = string.Empty;
            _additionalInfo = string.Empty;
            _imageBase64 = string.Empty;
            _imagePreview = string.Empty;
            _selectedDisciplineIds.Clear();

            _adminPosition = string.Empty;
        }
    }
}