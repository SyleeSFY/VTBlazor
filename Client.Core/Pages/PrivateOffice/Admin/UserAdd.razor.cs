using Client.Core.Entities.Enums;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.PrivateOffice.Admin;

public partial class UserAdd : ComponentBase
{
    private Role _activeBtn = Role.student;

    private void SetActiveTable(Role level)
    {
        _activeBtn = level;
    }
}