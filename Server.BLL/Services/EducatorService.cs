using Server.BLL.Services.Inrerfaces;
using Server.DLL.Interfaces;
using Server.DLL.Models.Entities;
using Server.DLL.Models.Entities.Educator;

namespace Server.BLL.Services;

public class EducatorService : IEducatorService
{
    private readonly IEducatorRepository _educatorRepository;

    public EducatorService(IEducatorRepository educatorRepository)
    {
        _educatorRepository = educatorRepository;
    }

    public async Task<Educator> GetByIdAsync(int id)
    {
        return await _educatorRepository.GetByIdAsync(id);
    }
    
    public async Task<Educator> GetByIdAddInfoAsync(int id)
    {
        return await _educatorRepository.GetByIdAddInfoAsync(id);
    }
    
}