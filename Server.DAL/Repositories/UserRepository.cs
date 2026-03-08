using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UniversityDbContext _context;
    
    public UserRepository(UniversityDbContext context)
        => _context = context;
    
    /// <summary>
    /// Получение всего списка user's
    /// </summary>
    /// <returns></returns>
    public async Task<List<User>> GetUserAsync()
        => await _context.Users
            .Include(x => x.Educator)
            .Include(x => x.Student)
            .Include(x => x.Administrator)
            .ToListAsync();
}