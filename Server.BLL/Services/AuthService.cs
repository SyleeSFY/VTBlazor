using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.BLL.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private User _user;

        public AuthService(IAuthRepository educatorRepository)
            => _authRepository = educatorRepository;

        public async Task<AuthResponce> CheckUserAvaiblebyTokenAsync(AuthData data)
            => await CheckUserAvaibleAsync(data.Email, data.Password) ? 
            new AuthResponce() { 
                Success = true, 
                JwtToken = GenerateJwtToken(_user) 
            } 
            : new AuthResponce();

        public async Task<bool> CheckUserAvaibleAsync(string email, string password)
        {
            _user = await _authRepository.GetUserByEmailAsync(email);
            if (_user != null && BCrypt.Net.BCrypt.Verify(password, _user.PasswordHash))
                return true;
            return false;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("QsA1!2cASo0O3FVCXs2saQsxcA@1SA8XCbvcDFQWAasx123;,9aQxzc4vcSx3bvc2jj"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "VT",
                audience: "VT",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
