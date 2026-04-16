using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;
using Server.DAL.Models.Enums;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        private readonly IEducatorService _educatorService;
        private readonly IFileService _fileService;
        private readonly IUserService _userService;

        public FileController(IEducatorService educatorService, IFileService fileService, IUserService userService)
        {
            _educatorService = educatorService;
            _fileService = fileService;
            _userService = userService;
        }

        [HttpGet("GetEducatorTask/{id}")]
        public async Task<TaskEducation> GetEducatorTask(int id)
        {
            var users = await _educatorService.GetTasksEducatorById(id);
            return users;
        }

        [HttpGet("GetEducatorTaskByGroup/{id}")]
        public async Task<List<TaskEducation>> GetEducatorTaskByGroup(int id)
        {
            var users = await _educatorService.GetEducatorTaskByGroup(id);
            return users;
        }

        [HttpGet("GetTasksEducatorByIdSimple/{EducatorId}")]
        public async Task<List<TaskEducation>> GeTasksByIdSimple(int EducatorId)
        {
            return await _educatorService.GetTasksEducatorByIdSimple(EducatorId);
        }

        [HttpGet("GetSolutionByTaskIdSimple/{taskId}")]
        public async Task<List<StudentSolution>> GetSolutionsByTaskIdSimple(int taskId)
        {
            return await _userService.GetSolutionsStudentByTaskIdSimple(taskId);
        }

        [HttpPost("UpdateSolutionStatus/{Id}")]
        public async Task<bool> GetSolutionsByTaskIdSimple(int Id, SolutionStudentDTO solution)
        {
            return await _userService.UpdateSolutionStatus(Id, solution.Status);
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

        [HttpPost("PostAddSolution")]
        public async Task<ActionResult<bool>> PostAddSolution(SolutionStudentDTO data)
        {
            if (data != null)
            {
                var result = await _userService.AddSolutionByDTOAsync(data);
                //return result ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }

        [HttpGet("GetTaskFile/{fileId}")]
        public async Task<byte[]> GetTaskFile(int fileId)
        {
            if (fileId > 0)
            {
                var result = await _fileService.GetFile(fileId, FileType.Task);
                return result;
            }
            return Array.Empty<byte>();
        }

        [HttpGet("GetSolutionFile/{fileId}")]
        public async Task<byte[]> GetSolutionFile(int fileId)
        {
            if (fileId > 0)
            {
                var result = await _fileService.GetFile(fileId, FileType.Solution);
                return result;
            }
            return Array.Empty<byte>();
        }
        #region Message

        [HttpPost("PostAddMessage")]
        public async Task<ActionResult<bool>> PostAddMessage(MessageInChatDTO data)
        {
            if (data != null)
            {
                var result = await _userService.AddMessageByDTOAsync(data);
                //return result ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }
        
        #endregion
    }
}
