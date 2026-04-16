using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Client.Core.Entities.Interfaces;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class DiciplineAdd : ComponentBase
    {
        [Parameter]  public int? DiciplineId { get; set; }
        [Inject] private IApiService _apiService { get; set; }

        private string _diciplineName;
        private int? _diciplineCourse = null;
        
        private bool _diciplineIsMaga;
        private bool _isEditMode = false;
        private bool _isError = false;

        private Discipline? _discipline;
        
        private TrainedGroup _diciplineTrainedGroup;

        public DiciplineAdd()
        {
            _diciplineTrainedGroup = new TrainedGroup();
            _discipline = new Discipline();
        }

        protected override async Task OnInitializedAsync()
        {
            _discipline = DiciplineId is not null ? await _apiService.GetDisciplineById((int)DiciplineId) : null;
            _isEditMode =  _discipline.Id is not 0 ? true : false;

            if (_isEditMode)
                await FillFieldInEditMode(_discipline);
        }

        private async Task OnClickAddDiscipline()
        {
            var newDiscipline = await CreateNewDiscipline();
            var response = await _apiService.PostAddDiscipline(newDiscipline);
            if (response)
            {
                Navigation.NavigateTo($"/DiciplineTable", true);
            }
            else
            {
                _isError =  true;
            }
        }
        
        private async Task OnClickEditDiscipline()
        {
            if (!_isEditMode)
            {
                await OnClickAddDiscipline();
                return;
            }
            
            var newDiscipline = await CreateNewDiscipline();
            var response = await _apiService.PostEditDiscipline(_discipline.Id,newDiscipline);
            if (response)
            {
                Navigation.NavigateTo($"/DiciplineTable", true);
            }
            else
            {
                _isError =  true;
            }
        }

        private async Task FillFieldInEditMode(Discipline dicipline)
        {
            _diciplineName = dicipline.NameDiscipline;
            _diciplineCourse = dicipline.Course;
            _diciplineIsMaga = dicipline.isMagistracy;
            _diciplineTrainedGroup.isAS = dicipline.Group.isAS;
            _diciplineTrainedGroup.isVM = dicipline.Group.isVM;
            _diciplineTrainedGroup.isPO = dicipline.Group.isPO;
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
