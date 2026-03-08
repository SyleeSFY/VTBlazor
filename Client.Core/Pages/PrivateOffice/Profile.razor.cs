using Client.Core.Entities.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Client.Core.Pages.PrivateOffice;

public partial class Profile : ComponentBase
{
    [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

    private int _userId;
    private string _userEmail;
    private string _userRole;
    private bool _isAuthenticated;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthStateTask;
        var user = authState.User;

        _isAuthenticated = user.Identity?.IsAuthenticated ?? false;

        if (_isAuthenticated)
        {
            var idString = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            int.TryParse(idString, out _userId);

            _userEmail = user.FindFirst(ClaimTypes.Email)?.Value;
            _userRole = user.FindFirst(ClaimTypes.Role)?.Value;
        }
    }
}