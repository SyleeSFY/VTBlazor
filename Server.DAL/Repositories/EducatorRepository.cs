using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Interfaces;

namespace Server.DAL.Repositories;

public class EducatorRepository : IEducatorRepository
{
    private readonly UniversityDbContext _context;
    
    public EducatorRepository(UniversityDbContext context)
        => _context = context;
    
    /// <summary>
    /// Получение всего списка user's
    /// </summary>
    /// <returns></returns>
    public async Task<List<Educator>> GetEducatorsSimpleAsync()
        => await _context.Educators.ToListAsync();
    
    public async Task<List<Educator>> GetEducatorsAsync()
        => await _context.Educators
            .Include(x => x.EducatorAdditionalInfo)
                .ThenInclude(ai => ai.EducatorDisciplines)
                    .ThenInclude(ed => ed.Discipline)
            .ToListAsync();

    public async Task<Educator> GetByIdSimpleAsync(int id)
        => await _context.Educators.FindAsync(id);
    
    public async Task<Educator> GetByIdAsync(int id)
        => await _context.Educators
            .Include(x => x.EducatorAdditionalInfo)
                .ThenInclude(ai => ai.EducatorDisciplines)
                    .ThenInclude(ed => ed.Discipline)
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<List<Discipline>> GetDiciplinesAsync()
    => await _context.Disciplines
            .Include(x => x.Group)
            .ToListAsync();

    public async Task AddEducator(Educator edc)
    {
        await _context.Educators.AddAsync(edc);
        await _context.SaveChangesAsync();   
    }
}