using Client.Core.Entities.Models.DTO;
using Client.Core.Entities.Models.Education;
using Client.Core.Entities.Models.User;
using Client.Core.Entities.Models.User.Dicipline;
using Client.Core.Entities.Models.User.EducatorModel;
using Microsoft.AspNetCore.Components.Authorization;
using static System.Net.WebRequestMethods;

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
        Task<User> GetUserByUserId(int id);
        Task<Student> GetStudentById(int id);
        Task<byte[]> GetFileByte(int fileId);
        Task<HttpResponseMessage> PostSolutionStudent(SolutionStudentDTO solution);
        Task<List<StudentSolution>> GetSolutionByTaskIdSimple(int taskId);
        Task<List<User>> GetUsersStudentByGroup(int groupId);
        Task<StudentSolution> GetSolutionByTaskIdAndStudentId(int taskId, int studentId);
        Task<StudentSolution> GetSolutionById(int id);
        Task<byte[]> GetSolutionFileByte(int fileId);
        Task<bool> UpdateSolutionStatus(int solutionId, SolutionStudentDTO updateData);
        Task<Student> GetStudentByStudentId(int StudentId);
        Task<bool> PostAddGroup(GroupDTO group);
        Task<Group> GetGroupById(int groupId);
        Task<bool> UpdateGroup(int groupId, GroupDTO group);
        Task<User> GetUserWithStudentInfoById(int id);
        Task<User> GetUserWithEducatorInfoById(int id);
        Task<User> GetUserWithAdminInfoById(int id);
        Task<bool> PostEditUser(int userId, UserDTO user);
        Task<bool> PostAddUser(UserDTO user);
        Task<bool> PostAddDiscipline(DisciplineDTO discipline);
        Task<bool> PostEditDiscipline(int disciplineId, DisciplineDTO discipline);
        Task<User> GetUserByAutomaticallyUserId(int id);
        Task<HttpResponseMessage> PostMessage(MessageInChatDTO message);
        Task<User> GetUserByAuth(AuthenticationState authState);
    }
}