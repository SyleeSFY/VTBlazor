using Client.Core.Shared.Models;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages
{
    public partial class Disciplines : ComponentBase
    {
        private List<Discipline> _educators = new List<Discipline>();

        protected override async Task OnInitializedAsync()
            => _educators = await GetEducators();

        public async Task<List<Discipline>> GetEducators()
        {
            try
            {
                _educators = await Http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDicipline");
                return _educators;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
