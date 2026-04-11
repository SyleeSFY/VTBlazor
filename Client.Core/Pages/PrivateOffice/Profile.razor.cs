using Client.Core.Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;
using System.Security.Claims;
using Client.Core.Entities.Enums;
using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;

namespace Client.Core.Pages.PrivateOffice;

public partial class Profile : ComponentBase
{
    [Inject] private IApiService _apiService { get; set; }
    [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

    private Role _userRole;
    private User _user;

    private int _userId;
    private string _userEmail = string.Empty;
    private bool _isAuthenticated;

    public Profile()
    {
        _user = new User();
    }

    protected override async Task OnInitializedAsync()
    {
        await ParseCookie();
        _user = await _apiService.GetUserByUserId(_userId);
    }

    private async Task<User> GetUser(Role role)
    {
        
        // switch (role)
        // {
        //     case Role.student:
        //         _user = await _apiService.GetStudentByAuth(await AuthStateTask);
        //         break;
        //     case Role.admin:
        //         break;
        //     case Role.educator:
        //         break;
        // }
        return new User();
    }
    private async Task ParseCookie()
    {
        var authState = await AuthStateTask;
        var user = authState.User;

        _isAuthenticated = user.Identity?.IsAuthenticated ?? false;

        if (_isAuthenticated)
        {
            var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(idString, out _userId);

            _userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
            _userRole = (Role)Int32.Parse(user.FindFirst(ClaimTypes.Role)?.Value);
        }
    }
}