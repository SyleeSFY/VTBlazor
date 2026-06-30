using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.User;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Client.Core.Pages.PrivateOffice.Admin;

public partial class UsersTable : ComponentBase
{
    private List<User> _displayedUsers = new List<User>();
    private List<User> _allUsers = new List<User>();

    private List<Group> _groupsStudent = new List<Group>();

    private Group? _selectedGroup = null;
    private Role? _selectedRole = new Role?();

    protected override async Task OnInitializedAsync()
    {
        _allUsers = await GetUsers();
        _allUsers = _allUsers.OrderBy(x => x.Id).ToList();

        _displayedUsers = _allUsers;
        _groupsStudent = _allUsers
            .Where(x => x.Student?.Group != null)
            .Select(x => x.Student.Group)
            .DistinctBy(g => g.Id)
            .ToList();
    }

    private async Task OnRoleChanged(ChangeEventArgs e) {
        _selectedRole = e.Value?.ToString() switch {
            "student" => Role.student,
            "admin" => Role.admin,
            "educator" => Role.educator,
            _ => null
        };

        if (_selectedRole != Role.student)
            _selectedGroup = null;

        await ApplyFilters();
    }

    private async Task OnGroupChanged(ChangeEventArgs e) {
        _selectedGroup = _groupsStudent.FirstOrDefault(g => g.Id.ToString() == e.Value?.ToString());

        await ApplyFilters();
    }

    private async Task ApplyFilters() {
        var query = _allUsers;

        if (_selectedRole.HasValue) {
            query = query.Where(x => x.Role == _selectedRole.Value).ToList();

            if (_selectedRole.Value == Role.student && _selectedGroup is not null) {
                query = query.Where(x => x.Student.Group.Id == _selectedGroup.Id).ToList();
            }
        }
        else
            query = _allUsers;

        _displayedUsers = query;
    }

    private async Task BtnDelete(int userId)
    {

    }

    private string GetSelectedRoleString() {
        return _selectedRole switch {
            Role.student => "student",
            Role.admin => "admin",
            Role.educator => "educator",
            _ => ""
        };
    }

    private async Task<List<User>> GetUsers()
        => await Http.GetFromJsonAsync<List<User>>("api/User/GetUsers");
    
}