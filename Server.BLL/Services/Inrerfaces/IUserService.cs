using Server.DAL.Models.Entities.Users;

namespace Server.BLL.Services.Inrerfaces;

public interface IUserService
{
    Task<List<User>> GetUserAsync();
}