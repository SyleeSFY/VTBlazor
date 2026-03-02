using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailAsync(string email);
    }
}
