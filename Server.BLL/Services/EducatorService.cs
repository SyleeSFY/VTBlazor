using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;

namespace Server.BLL.Services;

public class EducatorService : IEducatorService
{
    private readonly IEducatorRepository _educatorRepository;

    public EducatorService(IEducatorRepository educatorRepository)
        => _educatorRepository = educatorRepository;
    
    /// <summary>
    /// Получение единичного преподавателя,
    /// со всеми связанными сущностями
    /// </summary>
    /// <param name="id"></param>
    public async Task<Educator> GetByIdAsync(int id)
    {
        return await _educatorRepository.GetByIdAsync(id);
    }
    
    /// <summary>
    /// Получение educator по id(упрощенный вариант)
    /// </summary>
    /// <param name="id"></param>
    public async Task<Educator> GetByIdSimpleAsync(int id)
    {
        return await _educatorRepository.GetByIdAsync(id);
    }

    public async Task<List<Educator>> GetEducatorsAsync()
    {
        var educators = await _educatorRepository.GetEducatorsAsync();
        if (educators.Count != 0)
            return educators;
        return new List<Educator>();
    }

    
    /// <summary>
    /// Упрощенный список educators
    /// </summary>
    /// <param name="id"></param>
    public async Task<List<Educator>> GetEducatorsSimpleAsync()
    {
        try
        {
            var educators = await _educatorRepository.GetEducatorsSimpleAsync();
            if (educators.Count != 0)
                return educators;
            return new List<Educator>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task AddEducator(Educator educator)
    {
        await _educatorRepository.AddEducator(educator);
    }
    
}