using System.Net.Http.Json;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.PrivateOffice.Admin;

public partial class UsersInfo : ComponentBase
{
    
    private async Task<List<User>> GetUsers()
    {
        return await Http.GetFromJsonAsync<List<User>>("api/Educators/GetEducators");;
    }
}