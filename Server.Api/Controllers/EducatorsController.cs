using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducatorsController : ControllerBase
{
    private readonly IEducatorService _educatorService;

    public EducatorsController(IEducatorService educatorService)
        => _educatorService = educatorService;
    
    [HttpGet("{id}")]
    public async Task<ActionResult<Educator>> GetById(int id)
    {
        var educator = await _educatorService.GetByIdAsync(id);
        if (educator == null)
            return NotFound();
        
        return educator;
    }

    [HttpGet("GetEducatorSimple/{id}")]
    public async Task<ActionResult<Educator>> GetSimpleByUserId(int id)
    {
        var educator = await _educatorService.GetSimpleByUserId(id);
        if (educator == null)
            return NotFound();
        
        return educator;
    }
    
    [HttpGet("GetEducatorsSimple")]
    public async Task<ActionResult<List<Educator>>> GetEducatorsSimple()
    {
        var educators =  await _educatorService.GetEducatorsSimpleAsync();
        if (educators.Count == 0)
            return NotFound();
        return educators;
    }
    
    [HttpGet("GetEducators")]
    public async Task<ActionResult<List<Educator>>> GetEducators()
    {
        var educators =  await _educatorService.GetEducatorsAsync();
        if (educators.Count == 0)
            return NotFound();
        return educators;
    }

    [HttpPost("educators")]
    public async Task AddEducator(Educator id)
    {
        await _educatorService.AddEducator(id);
    }
    
    [HttpGet("GetGroups")]
    public async Task<List<Group>> GetGroups()
    {
        var users =  await _educatorService.GetGroupsAsync();
        return users;
    }

    [HttpGet("GetEducatorTask/{id}")]
    public async Task<TaskEducation> GetEducatorTask(int id)
    {
        var users =  await _educatorService.GetTasksEducatorById(id);
        return users;
    }

    [HttpGet("GetTasksEducatorByIdSimple/{EducatorId}")]
    public async Task<List<TaskEducation>> GeTasksByIdSimple(int EducatorId)
    {
        return  await _educatorService.GetTasksEducatorByIdSimple(EducatorId);
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
        return new byte[0];
    }
    
}