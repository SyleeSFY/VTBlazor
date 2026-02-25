using System.Net.Http.Json;
using Client.Core.Entities.Models.User.Educator;
using Microsoft.AspNetCore.Components;

namespace Client.Core.Pages.Public;

public partial class Educators : ComponentBase
{
    private List<Educator> _educators = new List<Educator>();
    
    protected override async Task OnInitializedAsync()
        => _educators = await GetEducators();
    
    public async Task<List<Educator>> GetEducators()
    {
        _educators = await Http.GetFromJsonAsync<List<Educator>>("api/Educators/GetEducators");
        return _educators.OrderBy(x => x.Id).ToList();
    }
}