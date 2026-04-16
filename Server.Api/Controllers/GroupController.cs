using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;
using static System.Net.WebRequestMethods;

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

        [HttpPost("UpdateGroup/{groupId}")]
        public async Task<bool> UpdateGroup(int groupId, GroupDTO group)
            => await _educatorService.UpdateGroup(groupId, group);

        [HttpGet("GetGroups")]
        public async Task<List<Group>> GetGroups()
            => await _educatorService.GetGroupsAsync();

        [HttpGet("GetGroup/{groupId}")]
        public async Task<Group> GetGroupById(int groupId)
            => await _educatorService.GetGroupById(groupId);
    }
}