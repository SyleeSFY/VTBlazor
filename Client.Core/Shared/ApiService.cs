using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Client.Core.Pages.PrivateOffice.Admin;
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
            => await _http.GetFromJsonAsync<List<TaskEducation>>($"api/file/GetTasksEducatorByIdSimple/{educatorId}") ?? new List<TaskEducation>();
        
        public async Task<List<TaskEducation>> GetTasksEducatorByGroup(int educatorId)
            => await _http.GetFromJsonAsync<List<TaskEducation>>($"api/file/GetEducatorTaskByGroup/{educatorId}") ?? new List<TaskEducation>();

        public async Task<Educator> GetEducatorByIdSimple(int id)
            => await _http.GetFromJsonAsync<Educator>($"api/educators/GetEducatorSimple/{id}") ?? new Educator();

        public async Task<Student> GetStudentById(int id)
            => await _http.GetFromJsonAsync<Student>($"api/user/GetStudentByUserId/{id}") ?? new Student();
        
        public async Task<TaskEducation> GetTaskEducationById(int id)
            => await _http.GetFromJsonAsync<TaskEducation>($"api/file/GetEducatorTask/{id}") ?? new TaskEducation();

        public async Task<Educator> GetEducatorByAuth(AuthenticationState authState)
        {
            var id = await GetId(authState);

            if (id > 0)
                return await GetEducatorByIdSimple(id);

            return new Educator();
        }

        public async Task<Student> GetStudentByAuth(AuthenticationState authState)
        {
            var id = await GetId(authState);

            if (id > 0)
                return await GetStudentById(id);

            return new Student();
        }

        public async Task<byte[]> GetFileByte(int fileId)
            => await _http.GetFromJsonAsync<byte[]>($"api/file/GetTaskFile/{fileId}") ?? new byte[0];

        private async Task<int> GetId(AuthenticationState authState)
        {
            var user = authState.User;

            if (user?.Identity?.IsAuthenticated != true)
                return 0;

            var userIdClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userIdClaim, out int userId))
                return userId;
            return 0;
        }
    }
}