using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IEducatorService _educatorService;

        public FileController(IEducatorService educatorService)
            => _educatorService = educatorService;

        [HttpGet("GetEducatorTask/{id}")]
        public async Task<TaskEducation> GetEducatorTask(int id)
        {
            var users = await _educatorService.GetTasksEducatorById(id);
            return users;
        }

        [HttpGet("GetTasksEducatorByIdSimple/{EducatorId}")]
        public async Task<List<TaskEducation>> GeTasksByIdSimple(int EducatorId)
        {
            return await _educatorService.GetTasksEducatorByIdSimple(EducatorId);
        }

        [HttpPost("PostAddTask")]
        public async Task<ActionResult<bool>> PostAddTask(TaskEducationDTO data)
        {
            if (data != null)
            {
                var result = await _educatorService.AddTask(data);
                return result ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }

        [HttpGet("GetFile/{fileId}")]
        public async Task<byte[]> GeFile(int fileId)
        {
            if (fileId > 0)
            {
                var result = await _educatorService.GetFile(fileId);
                return result;
            }
            return Array.Empty<byte>();
        }
    }
}
