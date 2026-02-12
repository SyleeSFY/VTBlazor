using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;

namespace Server.BLL.Services.Inrerfaces;

public interface IEducatorService
{
    Task<Educator> GetByIdAsync(int id);
    Task<Educator> GetByIdSimpleAsync(int id);
    Task<List<Educator>> GetEducatorsSimpleAsync();
    Task<List<Educator>> GetEducatorsAsync();
    Task AddEducator(Educator educator);
}