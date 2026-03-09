using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin
{
    public partial class UserEdit
    {
        [Parameter]
        public required int UserId { get; set; }

        [Parameter]
        public required int RoleEdit { get; set; }
        private User? _user { get; set; }

        private Role _role;

        protected override async Task OnInitializedAsync()
        {
            _role = (Role)RoleEdit;
            _user = await GetUser(UserId);
        }

        private async Task<User> GetUser(int userId)
            => await Http.GetFromJsonAsync<User>($"api/User/GetUser/{userId}");
    }
}