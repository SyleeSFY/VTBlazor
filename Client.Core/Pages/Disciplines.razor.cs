using Client.Core.Shared.Enums.HomePage;
using Client.Core.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages
{
    public partial class Disciplines : ComponentBase
    {
        private List<Discipline> _diciplines = new List<Discipline>();
        private List<DisciplineIndex> _diciplinesBachelor = new List<DisciplineIndex>();
        private List<DisciplineIndex> _diciplinesMagistracy = new List<DisciplineIndex>();

        private EducationLevelEnum statusTable = EducationLevelEnum.Bachelor;

        protected override async Task OnInitializedAsync() { 
            _diciplines = await GetEducators();
            if (_diciplines.Any())
            {
                _diciplinesBachelor = _diciplines.Where(x => !x.isMagistracy).Select((item, index) => new DisciplineIndex{ Index = index + 1, Discipline = item }).ToList();
                _diciplinesMagistracy = _diciplines.Where(x => x.isMagistracy).Select((item, index) => new DisciplineIndex { Index = index + 1, Discipline = item }).ToList();
            }
        }

        private void SetActiveTable(EducationLevelEnum level)
            =>statusTable = (level == EducationLevelEnum.Bachelor) ? EducationLevelEnum.Bachelor : EducationLevelEnum.Magistracy;


        private async Task<List<Discipline>> GetEducators()
        {
            _diciplines = await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDicipline");
            return _diciplines;
        }

        private string PlusOrMinus(bool state)
            => state ? "+" : "-";

        private bool isLastTr(int index,int course, List<DisciplineIndex> _diciplines)
            => _diciplines.Where(x => x.Discipline.Course == course).Max(x => x.Index) == index;

    }
}
