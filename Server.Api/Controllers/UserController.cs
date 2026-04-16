using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Users;
using static System.Net.WebRequestMethods;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService educatorService)
        => _userService = educatorService;
    
    [HttpGet("GetUsers")]
    public async Task<List<User>> GetUsers()
    {
        var users =  await _userService.GetUsersAsync();
        return users;
    }

    [HttpGet("GetUser/{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user =  await _userService.GetUser(id);
        if (user.Id != 0)
            return Ok(user);
        return NotFound();
    }

    [HttpGet("GetUserByUserId/{id}")]
    public async Task<ActionResult<User>> GetUserByUserId(int id)
    {
        var user = await _userService.GetUserByUserId(id);
        if (user.Id != 0)
            return Ok(user);
        return NotFound();
    }

    [HttpGet("GetStudentById/{id}")]
    public async Task<ActionResult<Student>> GetStudentByUserId(int id)
    {
        var user = await _userService.GetStudentByUserId(id);
        if (user.Id != 0)
            return Ok(user);
        return NotFound();
    }

    [HttpGet("GetStudentByStudentId/{id}")]
    public async Task<ActionResult<Student>> GetStudentByStudentId(int id)
    {
        var user = await _userService.GetStudentByStudentId(id);
        if (user.Id != 0)
            return Ok(user);
        return NotFound();
    }

    [HttpGet("GetUserStudentByGroupId/{id}")]
    public async Task<List<User>> GetUserStudentByGroupId(int id)
    {
        return await _userService.GetUserStudentByGroupId(id);
       
    }

    [HttpPost("PostEditUser/{userId}")]
    public async Task<ActionResult<bool>> PostEditUser(int userId, UserDTO data)
    {
        if (data != null)
        {
            var result = await _userService.EditUserByDTOAsync(userId, data);
            return Ok(result);
        }
        return NotFound();
    }

    [HttpPost("PostAddUser")]
    public async Task<ActionResult<bool>> PostAddUser(UserDTO data)
    {
        if (data != null)
        {
            var result = await _userService.AddUserByDTOAsync(data);
            return Ok(result);
        }
        return NotFound();
    }
    #region FullInfo

    [HttpGet("GetUserByAutomaticallyUserId/{id}")]
    public async Task<User> GetUserByAutomaticallyUserId(int id)
    {
        return await _userService.GetUserWithAutInfoByUserId(id);

    }

    [HttpGet("GetUserWithStudentInfo/{id}")]
    public async Task<User> GetUserWithStudentInfoById(int id)
    {
        return await _userService.GetUserWithStudentInfoByUserId(id);

    }

    [HttpGet("GetUserWithEducatorInfoById/{id}")]
    public async Task<User> GetUserWithEducatorInfoById(int id)
    {
        return await _userService.GetUserWithEducatorInfoByUserId(id);

    }
    [HttpGet("GetUserWithAdminInfoById/{id}")]
    public async Task<User> GetUserWithAdminInfoById(int id)
    {
        return await _userService.GetUserWithAdminInfoByUserId(id);
    }

    #endregion
}