using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Education;
using Server.DAL.Models.Entities.Users;
using Server.DAL.Models.Enums;

namespace Server.BLL.Services.Inrerfaces;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUser(int userId);
    Task<bool> AddUserByDTOAsync(UserDTO userDTO);
    Task<User> GetUserByUserId(int userId);
    Task<Student> GetStudentByStudentId(int userId);
    Task<bool> AddSolutionByDTOAsync(SolutionStudentDTO solutionDTO);
    Task<List<StudentSolution>> GetSolutionsStudentByTaskIdSimple(int taskId);
    Task<Student> GetStudentByUserId(int userId);
    Task<List<User>> GetUserStudentByGroupId(int userId);
    Task<bool> UpdateSolutionStatus(int solutionId, SolutionStatus status);
}