using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Pages.Public;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class DiciplineTable : ComponentBase
    {
        private List<Discipline> _diciplines = new List<Discipline>();

        protected override async Task OnInitializedAsync()
        {
            _diciplines = await GetEducators();
        }

        private async Task<List<Discipline>> GetEducators()
        {
            _diciplines = await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines");
            return _diciplines;
        }

        private async void BtnDelete(int userId)
        {

        }

        private string PlusOrMinus(bool state)
            => state ? "+" : "–";
    }
}
