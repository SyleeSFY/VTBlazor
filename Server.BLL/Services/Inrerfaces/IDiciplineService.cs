using Server.DAL.Models.Entities.Educators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BLL.Services.Inrerfaces
{
    public interface IDiciplineService
    {
        Task<List<Discipline>> GetDiciplinesAsync();
    }
}
