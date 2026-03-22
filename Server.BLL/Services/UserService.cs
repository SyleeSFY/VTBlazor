using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;
using Server.DAL.Models.DTO;
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
                    user.Administrator = new Admin()
                    {
                        Position = userDTO.Administrator.Position
                    };
                    break;
                case Role.educator:
                    user.Educator = new Educator()
                    {
                        Profession = userDTO.Educator.Profession,
                        AcademicDegree = userDTO.Educator.AcademicDegree,
                        EducatorAdditionalInfo = new EducatorAdditionalInfo
                        {
                            EducationLevel = userDTO.Educator.EducatorAdditionalInfo.EducationLevel,
                            SpecialtyOrFieldOfStudy = userDTO.Educator.EducatorAdditionalInfo.SpecialtyOrFieldOfStudy,
                            Qualification = userDTO.Educator.EducatorAdditionalInfo.Qualification,
                            AdditionalInfo = userDTO.Educator.EducatorAdditionalInfo.AdditionalInfo,
                            Image = Convert.FromBase64String(userDTO.Educator.EducatorAdditionalInfo.Image),
                            AcademicTitle = userDTO.Educator.EducatorAdditionalInfo.AcademicTitle,
                            EducatorDisciplines = userDTO.Educator.EducatorAdditionalInfo.EducatorDisciplines
                            .Select(x => new EducatorDiscipline() { DisciplineId = x.DisciplineId })
                            .ToList()
                        }
                    };
                break;
            case Role.student:
                    user.Student = new Student()
                    {
                        GroupId = userDTO.Student.GroupId,
                        StudentId = userDTO.Student.StudentCardId
                    };

                break;
            default:
                break;
        }
        return user;
        }
    }
}