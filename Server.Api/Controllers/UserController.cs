using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;

namespace Server.Api.Controllers;

public class UserController
{
    private readonly IUserService _userService;

    public UserController(IUserService educatorService)
        => _userService = educatorService;
    
    [HttpGet("GetUsers")]
    public async Task<List<User>> GetUsers()
    {
        var users =  await _userService.GetUserAsync();
        // if (users.Count == 0)
        //     return NotFound();
        return users;
    }
}