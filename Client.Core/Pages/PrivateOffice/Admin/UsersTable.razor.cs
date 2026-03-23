using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin;

public partial class UsersTable : ComponentBase
{
    private List<User> _users = new List<User>();

    protected override async Task OnInitializedAsync()
    {
        _users = await GetUsers();
        _users = _users.OrderBy(x => x.Id).ToList();
    }

    private async void BtnDelete(int userId)
    {

    }

    private async Task<List<User>> GetUsers()
        => await Http.GetFromJsonAsync<List<User>>("api/User/GetUsers");
    
}