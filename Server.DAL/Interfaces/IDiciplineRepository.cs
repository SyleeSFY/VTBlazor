using Client.Core.Entities.Models.DTO;
using Server.DAL.Models.Entities.Educators;

namespace Server.DAL.Interfaces
{
    public interface IDiciplineRepository
    {
        Task<List<Discipline>> GetDiciplinesAsync();
        Task<Discipline> GetDiciplineByIdAsync(int diciplineId);
        Task<bool> AddDiciplineAsync(Discipline dicipline);
    }
}
