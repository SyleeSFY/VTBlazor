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

        protected override async Task OnInitializedAsync()
        {
            _user = await GetUser(UserId);
        }

        private async Task<User> GetUser(int userId)
        {
            return await Http.GetFromJsonAsync<User>($"api/User/GetUser/{userId}");
        }
    }
}