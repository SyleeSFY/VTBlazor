using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class DiciplineEdit : ComponentBase
    {
        [Parameter]
        public required int DiciplineId { get; set; }
        private Discipline _discipline { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _discipline = await GetDiscipline(DiciplineId);
        }

        private async Task<Discipline> GetDiscipline(int disciplineId)
            => await Http.GetFromJsonAsync<Discipline>($"api/Diciplines/GetDicipline/{disciplineId}");
    }
}
