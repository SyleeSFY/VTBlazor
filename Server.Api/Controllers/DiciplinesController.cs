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
            return Ok(discipline);
        }

        #region PostEndPoint

        [HttpPost("PostAddDiscipline")]
        public async Task<ActionResult<bool>> PostAddDiscipline(DisciplineDTO data)
        {
            if (data != null)
            {
                var result = await _diciplineService.AddDiciplineByDTOAsync(data);
                return result.Id is not 0 ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }
        
        [HttpPost("PostEditDiscipline/{disciplineId}")]
        public async Task<ActionResult<bool>> PostEditDiscipline(int disciplineId, DisciplineDTO data)
        {
            if (data != null)
            {
                var responce = await _diciplineService.EditDiciplineByDTOAsync(disciplineId, data);
                return Ok(responce);
            }
            return NotFound();
        }
        
        #endregion

        [HttpDelete("DeleteDiscipline/{disciplineId}")]
        public async Task<ActionResult<bool>> DeleteDiscipline(int disciplineId)
        {
            if (disciplineId > 0)
            {
                var result = await _diciplineService.DeleteDiciplineAsync(disciplineId);
                return result ? Ok(result) : NotFound(result);
            }
            return NotFound();
        }
    }
}