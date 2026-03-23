using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;

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

        public async Task<Discipline> GetDiciplineByIdAsync(int diciplineId)
            => await _context.Disciplines
                .Include(x => x.Group)
                .FirstOrDefaultAsync(x => x.Id == diciplineId);

        public async Task<bool> DeleteDiciplineAsync(Discipline dicipline)
        {
            try
            {
                _context.Disciplines.Remove(dicipline);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        } 

        public async Task<bool> AddDiciplineAsync(Discipline dicipline)
        {
            try
            {
                await _context.Disciplines.AddAsync(dicipline);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
            
    }
}
