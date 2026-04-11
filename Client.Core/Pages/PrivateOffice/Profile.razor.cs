using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
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
        _userRole = Role.student;
        _user = new User();
    }

    protected override async Task OnInitializedAsync()
    {
        await ParseCookie();
        _user = await GetUser(_userId, _userRole);
    }

    private async Task<User> GetUser(int userId, Role role)
    {
        switch (role)
        {
            case Role.educator:
                return await _apiService.GetUserWithEducatorInfoById(userId);
            case Role.student:
                return await _apiService.GetUserWithStudentInfoById(userId);
            case Role.admin:
                return await _apiService.GetUserWithAdminInfoById(userId);
            default:
                throw new ArgumentException();
        }
    }

    private string GetGreeting() 
        => _user == null 
        ? "" 
        : $"{(_user.LastName ?? "")} {(_user.FirstName ?? "")} {(_user.MiddleName ?? "")}".Trim();

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