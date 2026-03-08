using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.Entities.Users;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
            => _authService = authService;

        [HttpPost("PostAuthUser")]
        public async Task<ActionResult<AuthResponce>> PostAuthUser(AuthData data)
        {
            if (data != null) {
                var result = await _authService.CheckUserAvaiblebyTokenAsync(data);
                return result.Success ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }
    }
}