using Server.DLL.Models.Entities;
using Server.DLL.Models.Entities.Educator;

namespace Server.BLL.Services.Inrerfaces;

public interface IEducatorService
{
    Task<Educator> GetByIdAsync(int id);
    Task<Educator> GetByIdAddInfoAsync(int id);
    
}