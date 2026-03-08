using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Interfaces;

public interface IUserRepository
{
    Task<List<User>> GetUserAsync();
}