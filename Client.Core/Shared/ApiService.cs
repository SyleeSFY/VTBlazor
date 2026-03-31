using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Json;
using System.Security.Claims;

namespace Client.Core.Shared
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
            => _http = http;

        public async Task<List<Discipline>> GetDisciplines()
            => await _http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines") ?? new List<Discipline>();
        public async Task<Discipline> GetDisciplineById(int id)
            => await _http.GetFromJsonAsync<Discipline>($"api/Diciplines/GetDicipline/{id}") ?? new Discipline();

        public async Task<List<Group>> GetGroups()
            => await _http.GetFromJsonAsync<List<Group>>("api/educators/GetGroups") ?? new List<Group>();
        
        public async Task<List<TaskEducation>> GetTasksEducatorByIdSimple(int educatorId)
            => await _http.GetFromJsonAsync<List<TaskEducation>>($"api/educators/GetTasksEducatorByIdSimple/{educatorId}") ?? new List<TaskEducation>();

        public async Task<Educator> GetEducatorByIdSimple(int id)
            => await _http.GetFromJsonAsync<Educator>($"api/educators/GetEducatorSimple/{id}") ?? new Educator();
        
        public async Task<TaskEducation> GetTaskEducationById(int id)
            => await _http.GetFromJsonAsync<TaskEducation>($"api/educators/GetEducatorTask/{id}") ?? new TaskEducation();

        public async Task<Educator> GetEducatorByAuth(AuthenticationState authState)
        {
            var user = authState.User;

            if (user?.Identity?.IsAuthenticated != true)
                return new Educator();

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return await GetEducatorByIdSimple(userId);

            return new Educator();
        }
        
        public async Task<byte[]> GetFileByte(int fileId)
            => await _http.GetFromJsonAsync<byte[]>($"api/educators/GetFile/{fileId}") ?? new byte[0];
    }
}