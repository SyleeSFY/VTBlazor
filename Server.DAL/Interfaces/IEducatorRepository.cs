using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;

namespace Server.DAL.Interfaces;

public interface IEducatorRepository
{
    Task<List<Educator>> GetEducatorsSimpleAsync();
    Task<List<Educator>> GetEducatorsAsync();

    Task<Educator> GetByIdSimpleAsync(int id);
    Task<Educator> GetByIdAsync(int id);
    Task AddEducator(Educator edc);
}