using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BLL.Services.Inrerfaces
{
    public interface IAuthService
    {
        Task<AuthResponce> CheckUserAvaiblebyTokenAsync(AuthData token);
        Task<bool> CheckUserAvaibleAsync(string email, string password);
    }
}
