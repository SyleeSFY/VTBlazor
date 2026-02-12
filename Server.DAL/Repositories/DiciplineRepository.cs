using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Repositories
{
    public class DiciplineRepository : IDiciplineRepository
    {
        private readonly UniversityDbContext _context;

        public DiciplineRepository(UniversityDbContext context)
            => _context = context;

        public async Task<List<Discipline>> GetDiciplinesAsync()
        => await _context.Disciplines
            .Include(x => x.Group)
            .ToListAsync();
    }
}
