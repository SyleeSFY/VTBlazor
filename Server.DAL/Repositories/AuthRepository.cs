using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Users;

namespace Server.DAL.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UniversityDbContext _context;

        public AuthRepository(UniversityDbContext context)
            => _context = context;

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(x => x.Email == email);

        //var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == tokenUser.Email && x.PasswordHash == tokenUser.AccessToken);

        //if (user != null)
        //    return user;
        //return null;





    }
}
