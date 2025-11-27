using Microsoft.EntityFrameworkCore;
using Server.DLL.Context.ApplicationDbContext;
using Server.DLL.Interfaces;
using Server.DLL.Models.Entities;
using Server.DLL.Models.Entities.Educator;

namespace Server.DLL.Repositories;

public class EducatorRepository : IEducatorRepository
{
    private readonly ApplicationDbContext _context;

    public EducatorRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Educator> GetByIdAsync(int id)
    {
        return await _context.Educators.FindAsync(id);
    }
    
    public async Task<Educator> GetByIdAddInfoAsync(int id)
    {
        return await _context.Educators
            .Include(x => x.EducatorAdditionalInfo)
                .ThenInclude(ai => ai.EducatorDisciplines)
                    .ThenInclude(ed => ed.Discipline)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}