using System.Net.Http.Json;
using Client.Core.Shared.Models;

namespace Client.Core.Pages;

public partial class Educators
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