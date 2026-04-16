using Client.Core.Entities.Enums;
using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Pages.Public;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class UserAdd : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }
        [Parameter] public int? UserId { get; set; }

        private Role _activeRole = Role.student;

        private string _lastName = string.Empty;
        private string _firstName = string.Empty;
        private string _middleName = string.Empty;
        private string _password = string.Empty;
        private string _email = string.Empty;

        private string _studentCardId = string.Empty;

        private string _profession = string.Empty;
        private string _academicDegree = string.Empty;

        private string _educationLevel = string.Empty;
        private string _academicTitle = string.Empty;
        private string _specialty = string.Empty;
        private string _qualification = string.Empty;
        private string _additionalInfo = string.Empty;
        private string _imageBase64 = string.Empty;
        private string _imagePreview = string.Empty;

        private int? _studentGroup = null;

        private User? _user;

        private List<Discipline> _diciplines = new List<Discipline>();
        private List<Group> _groups = new List<Group>();
        private List<int> _selectedDisciplineIds = new List<int>();

        private string _adminPosition = string.Empty;
        private bool _isError = false;
        private bool _isEditMode = false;

        private Dictionary<string, string> _errors = new Dictionary<string, string>();
        private List<string> _validationErrors = new List<string>();

        public UserAdd()
        {
            _user = new User();
        }

        protected override async Task OnInitializedAsync()
        {
            _diciplines = await _apiService.GetDisciplines();
            _groups = await _apiService.GetGroups();
            _user = UserId != null ? await _apiService.GetUserByAutomaticallyUserId((int)UserId) : null;
            _isEditMode = _user?.Id != 0;
            if (_isEditMode)
                await FillField(_user);
        }

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
            _isError = false;

            if (!ValidateForm())
            {
                return;
            }

            var user = await CreateUserWithInfo();

            var response = _isEditMode ? await _apiService.PostEditUser(_user.Id,user) : await _apiService.PostAddUser(user);

            if (response)
            {
                Navigation.NavigateTo("/UserTable");
            }
            else
            {
                _isError = true;
                _validationErrors.Clear();
                _validationErrors.Add("Ошибка при отправке данных на сервер");
            }
        }

        private async Task<UserDTO> CreateUserWithInfo()
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
            return user;
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
                GroupId = _studentGroup,
                StudentCardId = _studentCardId
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

        private bool ValidateForm()
        {
            _errors.Clear();
            _validationErrors.Clear();

            // Проверка общих полей
            if (string.IsNullOrWhiteSpace(_lastName))
            {
                _errors["LastName"] = "Фамилия обязательна";
                _validationErrors.Add("Фамилия не заполнена");
            }

            if (string.IsNullOrWhiteSpace(_firstName))
            {
                _errors["FirstName"] = "Имя обязательно";
                _validationErrors.Add("Имя не заполнено");
            }

            if (string.IsNullOrWhiteSpace(_middleName))
            {
                _errors["MiddleName"] = "Отчество обязательно";
                _validationErrors.Add("Отчество не заполнено");
            }

            if (_isEditMode)
            {
                if (!string.IsNullOrEmpty(_password) && _password.Length < 6)
                {
                    _errors["Password"] = "Пароль должен быть не менее 6 символов";
                    _validationErrors.Add("Пароль слишком короткий (минимум 6 символов)");
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(_password))
                {
                    _errors["Password"] = "Пароль обязателен";
                    _validationErrors.Add("Пароль не заполнен");
                }
                else if (_password.Length < 6)
                {
                    _errors["Password"] = "Пароль должен быть не менее 6 символов";
                    _validationErrors.Add("Пароль слишком короткий (минимум 6 символов)");
                }
            }

            if (string.IsNullOrWhiteSpace(_email))
            {
                _errors["Email"] = "Email обязателен";
                _validationErrors.Add("Email не заполнен");
            }
            else if (!_email.Contains("@") || !_email.Contains("."))
            {
                _errors["Email"] = "Некорректный email адрес";
                _validationErrors.Add("Email имеет неверный формат");
            }

            // Проверка полей в зависимости от роли
            switch (_activeRole)
            {
                case Role.student:
                    if (!_studentGroup.HasValue)
                    {
                        _errors["StudentGroup"] = "Выберите группу";
                        _validationErrors.Add("Не выбрана группа для студента");
                    }

                    if (string.IsNullOrWhiteSpace(_studentCardId))
                    {
                        _errors["StudentCardId"] = "Номер студенческого билета обязателен";
                        _validationErrors.Add("Номер студенческого билета не заполнен");
                    }
                    break;

                case Role.educator:
                    if (string.IsNullOrWhiteSpace(_profession))
                    {
                        _errors["Profession"] = "Должность обязательна";
                        _validationErrors.Add("Должность не заполнена");
                    }

                    if (string.IsNullOrWhiteSpace(_academicDegree))
                    {
                        _errors["AcademicDegree"] = "Ученая степень обязательна";
                        _validationErrors.Add("Ученая степень не выбрана");
                    }

                    if (string.IsNullOrWhiteSpace(_educationLevel))
                    {
                        _errors["EducationLevel"] = "Уровень образования обязателен";
                        _validationErrors.Add("Уровень образования не выбран");
                    }

                    if (string.IsNullOrWhiteSpace(_specialty))
                    {
                        _errors["Specialty"] = "Специальность обязательна";
                        _validationErrors.Add("Специальность не заполнена");
                    }

                    if (string.IsNullOrWhiteSpace(_qualification))
                    {
                        _errors["Qualification"] = "Квалификация обязательна";
                        _validationErrors.Add("Квалификация не заполнена");
                    }

                    if (_selectedDisciplineIds.Count == 0)
                    {
                        _errors["Disciplines"] = "Выберите хотя бы одну дисциплину";
                        _validationErrors.Add("Не выбраны преподаваемые дисциплины");
                    }
                    break;

                case Role.admin:
                    if (string.IsNullOrWhiteSpace(_adminPosition))
                    {
                        _errors["AdminPosition"] = "Должность обязательна";
                        _validationErrors.Add("Должность администратора не заполнена");
                    }
                    break;
            }

            return _validationErrors.Count == 0;
        }

        private async Task FillField(User user)
        {
            _activeRole = user.Role;
            _lastName = user.LastName;
            _firstName = user.FirstName;
            _middleName = user.MiddleName ?? string.Empty;
            _email = user.Email;
            _password = string.Empty;

            switch (user.Role)
            {
                case Role.student:
                    if (user.Student != null)
                    {
                        _studentGroup = user.Student.GroupId;
                        _studentCardId = user.Student.StudentCard;
                    }
                    break;

                case Role.educator:
                    if (user.Educator != null)
                    {
                        _profession = user.Educator.Profession;
                        _academicDegree = user.Educator.AcademicDegree ?? string.Empty;

                        if (user.Educator.EducatorAdditionalInfo != null)
                        {
                            var info = user.Educator.EducatorAdditionalInfo;
                            _educationLevel = info.EducationLevel;
                            _academicTitle = info.AcademicTitle;
                            _specialty = info.SpecialtyOrFieldOfStudy;
                            _qualification = info.Qualification;
                            _additionalInfo = info.AdditionalInfo;

                            if (!string.IsNullOrEmpty(info.Image))
                            {
                                _imageBase64 = info.Image;
                                _imagePreview = info.GetIMG();
                            }

                            _selectedDisciplineIds.Clear();
                            if (info.EducatorDisciplines != null)
                            {
                                _selectedDisciplineIds = info.EducatorDisciplines
                                    .Select(d => d.DisciplineId)
                                    .ToList();
                            }
                        }
                    }
                    break;

                case Role.admin:
                    if (user.Administrator != null)
                    {
                        _adminPosition = user.Administrator.Position;
                    }
                    break;
            }

            // Принудительное обновление UI (если требуется)
            await InvokeAsync(StateHasChanged);
        }

        private void SetActiveTable(Role level)
        {
            _activeRole = level;
            _errors.Clear();
            _validationErrors.Clear();
            _isError = false;
        }

        private void ResetForm()
        {
            _lastName = string.Empty;
            _firstName = string.Empty;
            _middleName = string.Empty;
            _password = string.Empty;
            _email = string.Empty;

            _studentGroup = null;
            _studentCardId = string.Empty;

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

            _errors.Clear();
            _validationErrors.Clear();
            _isError = false;
        }
    }
}