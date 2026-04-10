using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class GroupTable : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }

        private List<Group> _groups;

        public GroupTable()
        {
            _groups = new List<Group>();
        }

        protected override async Task OnInitializedAsync()
        {
            _groups = await _apiService.GetGroups();
        }
    }
}
