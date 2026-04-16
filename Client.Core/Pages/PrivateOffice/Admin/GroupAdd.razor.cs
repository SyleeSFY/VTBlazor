using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class GroupAdd : ComponentBase
    {
        [Inject] private IApiService _apiService { get; set; }
        [Parameter] public int? GroupId { get; set; }
        private Group _group;

        private bool _isError = false;
        private bool _isEditMode => GroupId.HasValue;

        private string _name = string.Empty;

        public GroupAdd()
        {
            _group = new Group();
        }

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

        private async Task OnClickEditGroup()
        {
            if (_group.Id == 0)
            {
                await OnClickAddGroup();
                return;
            }

            if (string.IsNullOrEmpty(_name))
            {
                _isError = true;
                return;
            }

            var updatedGroup = new GroupDTO()
            {
                Name = _name.Trim().ToUpper(),
            };

            var response = await _apiService.UpdateGroup(_group.Id, updatedGroup);

            if (response)
                Navigation.NavigateTo("/GroupTable");
            else
                _isError = true;

        }

        protected override async Task OnInitializedAsync()
        {
            if (_isEditMode)
            {
                _group = await _apiService.GetGroupById((int)GroupId);

                if (_group.Id == 0)
                    _isError = true;
                else
                {
                    _name = _group.Name;
                }
            }
        }
    }
}
