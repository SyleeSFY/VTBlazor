using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUsersAsync();
    Task<User> GetUserSimpleAsync(int userId);
    Task<User> GetUserAsync(int userId);
}