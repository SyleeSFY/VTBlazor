using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;
using Server.DAL.Models.Entities.Educators;

namespace Server.BLL.Services.Inrerfaces;

public interface IEducatorService
{
    Task<Educator> GetByIdAsync(int id);
    Task<Educator> GetSimpleByUserId(int userId);
    Task<Educator> GetByIdSimpleAsync(int id);
    Task<List<Educator>> GetEducatorsSimpleAsync();
    Task<List<Educator>> GetEducatorsAsync();
    Task AddEducator(Educator educator);
    Task<List<TaskEducation>> GetTasksEducatorByIdSimple(int EducatorId);
    Task<TaskEducation> GetTasksEducatorById(int id);
    Task<List<Group>> GetGroupsAsync();
    Task<bool> AddTask(TaskEducationDTO taskDto);
    Task<List<TaskEducation>> GetEducatorTaskByGroup(int id);
    Task<StudentSolution> GetSolutionByIdAsync(int id);
    Task<StudentSolution> GetSolutionByTaskIdAndStudentId(int taskId, int studentId);
}