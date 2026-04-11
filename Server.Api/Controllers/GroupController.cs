using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IEducatorService _educatorService;

        public GroupController(IEducatorService educatorService)
            => _educatorService = educatorService;

        [HttpPost("PostAddGroup")]
        public async Task<bool> AddGroup(GroupDTO group)
            => await _educatorService.AddGroup(group.Name);

        [HttpGet("GetGroups")]
        public async Task<List<Group>> GetGroups()
            => await _educatorService.GetGroupsAsync();
    }
}