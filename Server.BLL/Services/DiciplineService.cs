using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities.Educators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BLL.Services
{
    public class DiciplineService : IDiciplineService
    {
        private readonly IDiciplineRepository _diciplineRepository;

        public DiciplineService(IDiciplineRepository educatorRepository)
            => _diciplineRepository = educatorRepository;

        public async Task<List<Discipline>> GetDiciplinesAsync()
        {
            var diciplines = await _diciplineRepository.GetDiciplinesAsync();
            if (diciplines.Count != 0)
                return diciplines;
            return new List<Discipline>();
        }

        public async Task<Discipline> GetDiciplineAsync(int diciplineId)
        {
            var dicipline = await _diciplineRepository.GetDiciplineByIdAsync(diciplineId);
            if (dicipline is not null)
                return dicipline;
            return new Discipline();
        }
    }
}
