using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class GroupAdd : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }

        private string _name = string.Empty;
        private bool _isError = false;

        private async Task OnClickAddGroup()
        {
            //    if (string.IsNullOrEmpty(_name))
            //    {
            //        _isError = true;
            //        return;
            //    }

            var newGroup = new GroupDTO()
            {
                Name = _name,
            };

            var response = await _apiService.PostAddGroup(newGroup);

            if (response) { }
            //    if (response)
            //        Navigation.NavigateTo($"/GroupTable", true);
            //    else
            //        _isError = true;
        }
    }
}
