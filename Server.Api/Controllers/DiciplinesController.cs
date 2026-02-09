using Microsoft.AspNetCore.Mvc;
using Server.BLL.Services.Inrerfaces;
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


        [HttpGet("GetDicipline")]
        public async Task<ActionResult<List<Discipline>>> GetDicipline()
        {
            var disciplines = await _diciplineService.GetDiciplinesAsync();
            if (disciplines.Count == 0)
                return NotFound();
            return disciplines;
        }
    }
}
