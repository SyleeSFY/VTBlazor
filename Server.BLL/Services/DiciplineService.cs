using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities.Educators;

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

        public async Task<bool> DeleteDiciplineAsync(int diciplineId)
        {
            var dicipline = await _diciplineRepository.GetDiciplineByIdAsync(diciplineId);
            if (dicipline is not null)
            {
                return await _diciplineRepository.DeleteDiciplineAsync(dicipline);
            }
            return false;
        }

        public async Task<Discipline> AddDiciplineByDTOAsync(DisciplineDTO diciplineDTO)
        {
            var dicipline = await CreateDisciplineAsync(diciplineDTO);
            var diciplineResponce = await _diciplineRepository.AddDiciplineAsync(dicipline);
            if (dicipline is not null)
                return dicipline;
            return new Discipline();
        }

        private async Task<Discipline> CreateDisciplineAsync(DisciplineDTO diciplineDTO)
        {
            return new Discipline()
            {
                NameDiscipline = diciplineDTO.NameDiscipline,
                Course = diciplineDTO.Course,
                isMagistracy = diciplineDTO.isMagistracy,
                Group = new TrainedGroup()
                {
                    isAS = diciplineDTO.Group.isAS,
                    isPO = diciplineDTO.Group.isPO,
                    isVM = diciplineDTO.Group.isVM
                }
            };
        }
    }
}
