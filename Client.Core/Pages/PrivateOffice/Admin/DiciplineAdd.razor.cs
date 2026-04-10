using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class DiciplineAdd : ComponentBase
    {
        private string _diciplineName;
        private int? _diciplineCourse = null;
        private bool _diciplineIsMaga;
        private TrainedGroup _diciplineTrainedGroup;

        public DiciplineAdd()
        {
            _diciplineTrainedGroup = new TrainedGroup();
        }

        private async Task OnClickAddDicipline()
        {
            var newDiscipline = await CreateNewDiscipline();
            var response = await Http.PostAsJsonAsync($"api/Diciplines/PostAddDicipline", newDiscipline);
            //var result = await response.Content.ReadFromJsonAsync<AuthResponce>();

            //if (result != null && result.Success)
            //{
            //    var token = new Cookie()
            //    {
            //        Email = _authorization.Email,
            //        JWT = result.JwtToken,
            //        ExpiredAt = DateTime.Now.AddDays(1)
            //    };
            //}
            //else
            //    var qwe = _diciplineName;
        }
        private async Task<DisciplineDTO> CreateNewDiscipline()
            => new DisciplineDTO()
            {
                NameDiscipline = _diciplineName,
                Course = (int)_diciplineCourse,
                isMagistracy = _diciplineIsMaga,
                Group = new TrainedGroupDTO()
                {
                    isAS = _diciplineTrainedGroup.isAS,
                    isPO = _diciplineTrainedGroup.isPO,
                    isVM = _diciplineTrainedGroup.isVM
                }
            };
    }
}
