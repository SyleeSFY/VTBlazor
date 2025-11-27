using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DLL.Models.Entities;
using Server.DLL.Models.Entities.Educator;

namespace Server.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EducatorsController : ControllerBase
{
    private readonly IEducatorService _educatorService;

    public EducatorsController(IEducatorService educatorService)
    {
        _educatorService = educatorService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Educator>> GetById(int id)
    {
        var educator = await _educatorService.GetByIdAddInfoAsync(id);
        if (educator == null)
            return NotFound();
        
        return educator;
    }
}