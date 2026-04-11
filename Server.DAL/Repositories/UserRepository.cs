using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;
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

    public async Task<List<User>> GetUsersStudentByGroupAsync(int groupId)
        => await _context.Users
            .Include(x => x.Student)
                .ThenInclude(x => x.Group).Where(x => x.Student.GroupId == groupId)
       
            .ToListAsync();

    public async Task<List<StudentSolution>> GetSolutionStudentByTaskIdSimpleAsync(int taskId)
        => await _context.StudentSolutions
            .Where(x => x.TaskId == taskId)
            .ToListAsync();

    public async Task<StudentSolution> GetSolutionByIdAsync(int solutionId)
        => await _context.StudentSolutions
            .FirstOrDefaultAsync(x => x.Id == solutionId);

    public async Task<bool> UpdateSolutionStatus(StudentSolution solution)
    {
        try
        {
            _context.StudentSolutions.Update(solution);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {

            return false;
        }

    }

    public async Task<User> GetUserSimpleAsync(int userId)
        => await _context.Users
            .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<User> GetUserAsync(int userId)
        => await _context.Users
            .Include(x => x.Educator)
            .Include(x => x.Student)
            .Include(x => x.Administrator)
            .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<User> GetUserByUserIdAsync(int userId)
    => await _context.Users
        .FirstOrDefaultAsync(x => x.Id == userId);

    public async Task<Student> GetStudentByStudentIdAsync(int studentId)
    => await _context.Students
        .Include(x => x.Group)
        .FirstOrDefaultAsync(x => x.Id == studentId);

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

    public async Task<int> AddSolutionAsync(StudentSolution solution)
    {
        try
        {
            await _context.StudentSolutions.AddAsync(solution);
            await _context.SaveChangesAsync();
            return solution.Id;
        }
        catch (Exception)
        {

            return 0;
        }
    }

    public async Task<User> GetUserWithStudentInfoByIdAsync(int id)
        => await _context.Users
                .Include(x => x.Student)
                    .ThenInclude(x => x.Group)
                .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetUserWithEducatorInfoByIdAsync(int id)
        => await _context.Users.Include(x => x.Educator).ThenInclude(x => x.EducatorAdditionalInfo).ThenInclude(x => x.EducatorDisciplines).ThenInclude(x => x.Discipline).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<User> GetUserWithAdminInfoByIdAsync(int id)
        => await _context.Users.Include(x => x.Administrator).FirstOrDefaultAsync(x => x.Id == id);

    
}