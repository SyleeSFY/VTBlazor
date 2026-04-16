using Client.Core.Entities.Interfaces;
using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.Education;
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

        #region Auth Helpers

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

        #endregion

        #region Discipline Endpoints

        public async Task<List<Discipline>> GetDisciplines()
            => await _http.GetFromJsonAsync<List<Discipline>>("api/Diciplines/GetDiciplines") ?? new List<Discipline>();

        public async Task<Discipline> GetDisciplineById(int id)
            => await _http.GetFromJsonAsync<Discipline>($"api/Diciplines/GetDicipline/{id}") ?? new Discipline();

        public async Task<bool> PostAddDiscipline(DisciplineDTO discipline)
        {
            var response = await _http.PostAsJsonAsync($"api/Diciplines/PostAddDiscipline", discipline);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        
        public async Task<bool> PostEditDiscipline(int disciplineId, DisciplineDTO discipline)
        {
            var response = await _http.PostAsJsonAsync($"api/Diciplines/PostEditDiscipline/{disciplineId}", discipline);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        #endregion

        #region Educator Endpoints

        public async Task<Educator> GetEducatorByIdSimple(int id)
            => await _http.GetFromJsonAsync<Educator>($"api/educators/GetEducatorSimple/{id}") ?? new Educator();

        public async Task<Educator> GetEducatorByAuth(AuthenticationState authState)
        {
            var id = await GetId(authState);

            if (id > 0)
                return await GetEducatorByIdSimple(id);

            return new Educator();
        }
        #endregion

        #region Student Endpoints

        public async Task<Student> GetStudentById(int id)
            => await _http.GetFromJsonAsync<Student>($"api/user/GetStudentById/{id}") ?? new Student();

        public async Task<Student> GetStudentByStudentId(int StudentId)
            => await _http.GetFromJsonAsync<Student>($"api/user/GetStudentByStudentId/{StudentId}") ?? new Student();

        public async Task<Student> GetStudentByAuth(AuthenticationState authState)
        {
            var id = await GetId(authState);

            if (id > 0)
                return await GetStudentById(id);

            return new Student();
        }

        #endregion

        #region User Endpoints
        public async Task<User> GetUserByAuth(AuthenticationState authState)
        {
            var id = await GetId(authState);

            if (id > 0)
                return await GetUserByUserId(id);

            return new User();
        }
        public async Task<User> GetUserByUserId(int id)
            => await _http.GetFromJsonAsync<User>($"api/user/GetUserByUserId/{id}") ?? new User();

        public async Task<User> GetUserByAutomaticallyUserId(int id)
            => await _http.GetFromJsonAsync<User>($"api/user/GetUserByAutomaticallyUserId/{id}") ?? new User();

        public async Task<List<User>> GetUsersStudentByGroup(int groupId)
            => await _http.GetFromJsonAsync<List<User>>($"api/user/GetUserStudentByGroupId/{groupId}") ?? new List<User>();

        public async Task<User> GetUserWithStudentInfoById(int id)
            => await _http.GetFromJsonAsync<User>($"api/user/GetUserWithStudentInfo/{id}") ?? new User();

        public async Task<User> GetUserWithEducatorInfoById(int id)
            => await _http.GetFromJsonAsync<User>($"api/user/GetUserWithEducatorInfoById/{id}") ?? new User();

        public async Task<User> GetUserWithAdminInfoById(int id)
            => await _http.GetFromJsonAsync<User>($"api/user/GetUserWithAdminInfoById/{id}") ?? new User();
        
        public async Task<bool> PostAddUser(UserDTO user)
        {
            var response = await _http.PostAsJsonAsync($"api/User/PostAddUser", user);
            return await response.Content.ReadFromJsonAsync<bool>();
        }
        public async Task<bool> PostEditUser(int userId, UserDTO user)
        {
            try
            {
                var response = await _http.PostAsJsonAsync($"api/User/PostEditUser/{userId}", user);
                return await response.Content.ReadFromJsonAsync<bool>();
            }
            catch (Exception ex)
            {

                throw;
            }

        }
        #endregion

        #region Group Endpoints

        public async Task<List<Group>> GetGroups()
            => await _http.GetFromJsonAsync<List<Group>>("api/group/GetGroups") ?? new List<Group>();

        public async Task<Group> GetGroupById(int groupId)
            => await _http.GetFromJsonAsync<Group>($"api/group/GetGroup/{groupId}") ?? new Group();

        public async Task<bool> PostAddGroup(GroupDTO group)
        {
            var response = await _http.PostAsJsonAsync($"api/group/PostAddGroup", group);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        public async Task<bool> UpdateGroup(int groupId, GroupDTO group)
        {
            var response = await _http.PostAsJsonAsync($"api/group/UpdateGroup/{groupId}", group);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        #endregion

        #region Task Education Endpoints

        public async Task<TaskEducation> GetTaskEducationById(int id)
            => await _http.GetFromJsonAsync<TaskEducation>($"api/file/GetEducatorTask/{id}") ?? new TaskEducation();

        public async Task<List<TaskEducation>> GetTasksEducatorByIdSimple(int educatorId)
            => await _http.GetFromJsonAsync<List<TaskEducation>>($"api/file/GetTasksEducatorByIdSimple/{educatorId}") ?? new List<TaskEducation>();

        public async Task<List<TaskEducation>> GetTasksEducatorByGroup(int educatorId)
            => await _http.GetFromJsonAsync<List<TaskEducation>>($"api/file/GetEducatorTaskByGroup/{educatorId}") ?? new List<TaskEducation>();

        #endregion

        #region Student Solution Endpoints

        public async Task<StudentSolution> GetSolutionById(int id)
            => await _http.GetFromJsonAsync<StudentSolution>($"api/educators/GetSolutionById/{id}") ?? new StudentSolution();

        public async Task<StudentSolution> GetSolutionByTaskIdAndStudentId(int taskId, int studentId)
            => await _http.GetFromJsonAsync<StudentSolution>($"api/educators/GetSolution/{taskId}/{studentId}") ?? new StudentSolution();

        public async Task<List<StudentSolution>> GetSolutionByTaskIdSimple(int taskId)
            => await _http.GetFromJsonAsync<List<StudentSolution>>($"api/file/GetSolutionByTaskIdSimple/{taskId}") ?? new List<StudentSolution>();

        public async Task<HttpResponseMessage> PostSolutionStudent(SolutionStudentDTO solution)
            => await _http.PostAsJsonAsync($"api/file/PostAddSolution", solution);

        public async Task<bool> UpdateSolutionStatus(int solutionId, SolutionStudentDTO updateData)
        {
            var response = await _http.PostAsJsonAsync($"api/file/UpdateSolutionStatus/{solutionId}", updateData);
            return await response.Content.ReadFromJsonAsync<bool>();
        }

        #endregion

        public async Task<HttpResponseMessage> PostMessage(MessageInChatDTO message)
            => await _http.PostAsJsonAsync($"api/file/PostAddMessage", message);

        #region Message

        #endregion

        #region File Endpoints

        public async Task<byte[]> GetFileByte(int fileId)
            => await _http.GetFromJsonAsync<byte[]>($"api/file/GetTaskFile/{fileId}") ?? new byte[0];

        public async Task<byte[]> GetSolutionFileByte(int fileId)
            => await _http.GetFromJsonAsync<byte[]>($"api/file/GetSolutionFile/{fileId}") ?? new byte[0];

        #endregion
    }
}