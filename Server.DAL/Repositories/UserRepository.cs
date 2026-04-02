using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;
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
    public async Task<List<User>> GetUsersAsync()
        => await _context.Users
            .Include(x => x.Educator)
            .Include(x => x.Student)
                .ThenInclude(x => x.Group)
            .Include(x => x.Administrator)
            .ToListAsync();

    public async Task<User> GetUserSimpleAsync(int userId)
        => await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<User> GetUserAsync(int userId)
        => await _context.Users
            .Include(x => x.Educator)
            .Include(x => x.Student)
            .Include(x => x.Administrator)
            .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<Student> GetStudentByUserIdAsync(int userId)
    => await _context.Students
        .Include(x => x.Group)
        .FirstOrDefaultAsync(x => x.UserId == userId);

    public async Task<bool> AddUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {

            return false;
        }
    }
}