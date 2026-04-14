using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Educators;

namespace Server.BLL.Services.Inrerfaces
{
    public interface IDiciplineService
    {
        Task<List<Discipline>> GetDiciplinesAsync();
        Task<Discipline> GetDiciplineAsync(int diciplineId);
        Task<bool> DeleteDiciplineAsync(int diciplineId);
        Task<Discipline> AddDiciplineByDTOAsync(DisciplineDTO diciplineDTO);
        Task<bool> EditDiciplineByDTOAsync(int disciplineId, DisciplineDTO diciplineDTO);

    }
}
