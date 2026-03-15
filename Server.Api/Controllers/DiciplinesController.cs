using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Educators;

namespace Server.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DiciplinesController : ControllerBase
    {
        private readonly IDiciplineService _diciplineService;

        public DiciplinesController(IDiciplineService educatorService)
            => _diciplineService = educatorService;

        [HttpGet("GetDiciplines")]
        public async Task<ActionResult<List<Discipline>>> GetDicipline()
        {
            var disciplines = await _diciplineService.GetDiciplinesAsync();
            if (disciplines.Count == 0)
                return NotFound();
            return disciplines;
        }

        [HttpGet("GetDicipline/{diciplineId}")]
        public async Task<ActionResult<Discipline>> GetDicipline(int diciplineId)
        {
            var discipline = await _diciplineService.GetDiciplineAsync(diciplineId);
            if (discipline.Id != 0)
                return Ok(discipline);
            return NotFound();
        }

        [HttpPost("PostAddDicipline")]
        public async Task<ActionResult<bool>> PostAddDicipline(DisciplineDTO data)
        {
            if (data != null)
            {
                var result = await _diciplineService.AddDiciplineByDTOAsync(data);
                //return result.Success ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }
    }
}