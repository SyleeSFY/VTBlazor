using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Users;

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

    [HttpPost("PostAddUser")]
    public async Task<ActionResult<bool>> PostAddUser(UserDTO data)
    {
        //if (data != null)
        //{
        //    var result = await _diciplineService.AddDiciplineByDTOAsync(data);
        //    //return result.Success ? Ok(result) : NotFound(result);
        //}
        return NotFound();
    }
}