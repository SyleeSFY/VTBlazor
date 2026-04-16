using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;
using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserSimpleAsync(int userId);
    Task<User> GetUserAsync(int userId);
    Task<bool> AddUserAsync(User user);
    //Task<Student> GetStudentByUserIdAsync(int userId);
    Task<int> AddSolutionAsync(StudentSolution solution);
    Task<StudentSolution> GetSolutionByIdAsync(int solutionId);
    Task<bool> UpdateSolutionStatus(StudentSolution solution);
    Task<List<StudentSolution>> GetSolutionStudentByTaskIdSimpleAsync(int taskId);
    Task<List<User>> GetUsersStudentByGroupAsync(int groupId);
    Task<Student> GetStudentByUserIdAsync(int studentId);
    Task<Student> GetStudentByStudentIdAsync(int Id);
    Task<User> GetUserByUserIdAsync(int userId);
    Task<User> GetUserWithStudentInfoByIdAsync(int id);
    Task<User> GetUserWithEducatorInfoByIdAsync(int id);
    Task<User> GetUserWithAdminInfoByIdAsync(int id);
    Task<User?> GetUserFullInfoAsync(int userId);
    Task<bool> UpdateUserAsync(User user);
    Task<MessageInChat?> AddMessageAsync(MessageInChat userMessage);
}