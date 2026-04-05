using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Users;

namespace Server.BLL.Services.Inrerfaces;

public interface IUserService
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUser(int userId);
    Task<bool> AddUserByDTOAsync(UserDTO userDTO);
    Task<Student> GetStudentByUserId(int userId);
    Task<bool> AddSolutionByDTOAsync(SolutionStudentDTO solutionDTO);
}