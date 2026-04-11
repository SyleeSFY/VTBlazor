using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class GroupAdd : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }

        private bool _isError = false;

        private string _name = string.Empty;

        private async Task OnClickAddGroup()
        {
            if (string.IsNullOrEmpty(_name))
            {
                _isError = true;
                return;
            }

            var newGroup = new GroupDTO()
            {
                Name = _name.Trim().ToUpper(),
            };

            var response = await _apiService.PostAddGroup(newGroup);

            if (response)
                Navigation.NavigateTo("/GroupTable");
            else
                _isError = true;
        }
    }
}
