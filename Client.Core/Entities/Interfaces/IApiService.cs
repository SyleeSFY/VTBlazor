using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Entities.Interfaces
{
    public interface IApiService
    {
        Task<List<Discipline>> GetDisciplines();
        Task<Discipline> GetDisciplineById(int id);
        Task<List<Group>> GetGroups();
        Task<List<TaskEducation>> GetTasksEducatorByIdSimple(int educatorId);
        Task<Educator> GetEducatorByIdSimple(int id);
        Task<TaskEducation> GetTaskEducationById(int id);
        Task<Educator> GetEducatorByAuth(AuthenticationState authState);
        Task<Student> GetStudentByAuth(AuthenticationState authState);
        Task<List<TaskEducation>> GetTasksEducatorByGroup(int educatorId);
        Task<Student> GetStudentById(int id);
        Task<byte[]> GetFileByte(int fileId);
    }
}