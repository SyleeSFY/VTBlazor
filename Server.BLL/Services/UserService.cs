using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;

namespace Server.BLL.Services;

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
}