using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
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
}