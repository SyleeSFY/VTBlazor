using Server.DLL.Models.Entities;
using Server.DLL.Models.Entities.Educator;

namespace Server.DLL.Interfaces;

public interface IEducatorRepository
{
    Task<Educator> GetByIdAsync(int id);
    Task<Educator?> GetByIdAddInfoAsync(int id);
}