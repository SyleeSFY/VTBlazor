using Client.Core.App.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Security.Claims;

namespace Client.Core.App.Layout.Nav
{
    public partial class NavProfile : ComponentBase
    {
        [CascadingParameter] private Task<AuthenticationState> AuthStateTask { get; set; }

        private bool isAuth = false;
        private int role;
       
        protected override async Task OnInitializedAsync()
        {
            if (AuthStateTask is not null)
            {
                var authState = await AuthStateTask;
                isAuth = authState.User.Identity?.IsAuthenticated ?? false;
                if (isAuth)
                    role = int.Parse(authState.User.FindFirst(ClaimTypes.Role)?.Value);
            }
        }
    }
}
