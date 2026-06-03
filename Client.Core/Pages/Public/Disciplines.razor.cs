using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Shared.Enums.HomePage;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.Public {
    public partial class Disciplines : ComponentBase {
        private List<Discipline> _diciplines = new List<Discipline>();
        private List<DisciplineIndex> _diciplinesBachelor = new List<DisciplineIndex>();
        private List<DisciplineIndex> _diciplinesMagistracy = new List<DisciplineIndex>();

        private EducationLevelEnum statusTable = EducationLevelEnum.Bachelor;

        private bool _isLoading = true;

        protected override async Task OnInitializedAsync() {
            try {
                var disciplines = await GetDisciplines();

                if (disciplines.Any()) {
                    _diciplines = disciplines.OrderBy(x => x.Course).ThenBy(x => x.Id).ToList();

                    _diciplinesBachelor = _diciplines
                        .Where(x => !x.isMagistracy)
                        .Select((item, index) => new DisciplineIndex { Index = index + 1, Discipline = item })
                        .ToList();

                    _diciplinesMagistracy = _diciplines
                        .Where(x => x.isMagistracy)
                        .Select((item, index) => new DisciplineIndex { Index = index + 1, Discipline = item })
                        .ToList();
                }
            }
            catch (Exception ex) {
            }
            finally {
                _isLoading = false;
            }
        }

        private void SetActiveTable(EducationLevelEnum level) {
            statusTable = level;
        }

        private async Task<List<Discipline>> GetDisciplines() {
            var result = await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines");
            return result ?? new List<Discipline>();
        }

        private string PlusOrMinus(bool state)
            => state ? "+" : "–";

        private bool isLastTr(int index, int course, List<DisciplineIndex> disciplines)
            => disciplines.Where(x => x.Discipline.Course == course).Max(x => x.Index) == index;
    }
}