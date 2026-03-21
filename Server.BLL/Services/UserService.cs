using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Enums;

namespace Server.BLL.Services {

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository educatorRepository)
        => _userRepository = educatorRepository;
    
    public async Task<List<User>> GetUsersAsync()
    {
        var users = await _userRepository.GetUsersAsync();
        if (users is null || users.Count <= 0)
            return new List<User>();
        return users;
    }
    public async Task<User> GetUser(int userId)
    {
        var user = await _userRepository.GetUserAsync(userId);
        if (user is null)
            return new User();
        return user;
    }
    
    public async Task<bool> AddUserByDTOAsync(UserDTO userDTO)
    {
        var user = await ParseUserByDTOAsync(userDTO);
        if (user is null)
            return false;
        await _userRepository.AddUserAsync(user);
        return true;
    }

    private async Task<User> ParseUserByDTOAsync(UserDTO userDTO)
    {
        var user = new User()
        {
            Email = userDTO.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(userDTO.PasswordHash),
            FirstName = userDTO.FirstName, 
            LastName = userDTO.LastName, 
            MiddleName = userDTO.MiddleName, 
            Role = userDTO.Role
        };

            switch (userDTO.Role)
        {
            case Role.admin:
                user.Administrator = new Admin();
                user.Administrator.Position = userDTO.Administrator.Position;
                break;
            case Role.educator:
                user.Educator = new Educator();
                user.Educator.Profession = userDTO.Educator.Profession;
                user.Educator.AcademicDegree = userDTO.Educator.AcademicDegree;
                user.Educator.EducatorAdditionalInfo.EducationLevel = userDTO.Educator.EducatorAdditionalInfo.EducationLevel;
                user.Educator.EducatorAdditionalInfo.SpecialtyOrFieldOfStudy = userDTO.Educator.EducatorAdditionalInfo.SpecialtyOrFieldOfStudy;
                user.Educator.EducatorAdditionalInfo.Qualification = userDTO.Educator.EducatorAdditionalInfo.Qualification;
                user.Educator.EducatorAdditionalInfo.AdditionalInfo = userDTO.Educator.EducatorAdditionalInfo.AdditionalInfo;
                user.Educator.EducatorAdditionalInfo.Image = Convert.FromBase64String(userDTO.Educator.EducatorAdditionalInfo.Image);
                user.Educator.EducatorAdditionalInfo.AcademicTitle = userDTO.Educator.EducatorAdditionalInfo.AcademicTitle;
                user.Educator.EducatorAdditionalInfo.EducatorDisciplines = userDTO.Educator.EducatorAdditionalInfo.EducatorDisciplines
                    .Select(x => new EducatorDiscipline() { DisciplineId = x.DisciplineId})
                    .ToList();

                break;
            case Role.student:
                user.Student = new Student();
                user.Student.GroupId = userDTO.Student.GroupId;
                user.Student.StudentId = "123321321";
                break;
            default:
                break;
        }
        return user;
    
        //public string PasswordHash { get; set; }

        }
    }
}