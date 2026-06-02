using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class DiciplineTable : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }

        private List<Discipline> _diciplines = new List<Discipline>();

        protected override async Task OnInitializedAsync()
        {
            _diciplines = await _apiService.GetDisciplines();
            _diciplines = _diciplines.OrderBy(x => x.Course).ThenBy(x => x.Id).ToList();
        } 

        private async Task BtnDelete(int disciplineId)
        {
            if (disciplineId > 0)
            {
                var responce = await Http.DeleteAsync($"api/Diciplines/DeleteDiscipline/{disciplineId}");
                if (responce.IsSuccessStatusCode)
                    Navigation.NavigateTo($"/DiciplineTable", true);
            }
        }

        private string PlusOrMinus(bool state)
            => state ? "+" : "–";
    }
}
