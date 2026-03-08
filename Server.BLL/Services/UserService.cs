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
    
    public async Task<List<User>> GetUserAsync()
    {
        var users = await _userRepository.GetUserAsync();
        if (users is null || users.Count <= 0)
            return new List<User>();
        return users;
    }
}