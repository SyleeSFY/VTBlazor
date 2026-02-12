using Server.DAL.Models.Entities.Educators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Interfaces
{
    public interface IDiciplineRepository
    {
        Task<List<Discipline>> GetDiciplinesAsync();
    }
}
