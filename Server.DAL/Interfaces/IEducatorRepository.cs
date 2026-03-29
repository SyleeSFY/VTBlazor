using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;

namespace Server.DAL.Interfaces;

public interface IEducatorRepository
{
    Task<List<Educator>> GetEducatorsSimpleAsync();
    Task<List<Educator>> GetEducatorsAsync();

    Task<Educator> GetByIdSimpleAsync(int id);
    Task<Educator> GetByIdAsync(int id);
    Task<Educator> GetSimpleByUserId(int userId);
    Task AddEducator(Educator edc);
    Task<List<Group>> GetGroupsAsync();
    Task<List<TaskEducation>> GetTasksEducatorByIdSimple(int EducatorId);
    Task<TaskEducation> GetTasksEducatorById(int id);
    Task<int> AddTask(TaskEducation task);
    Task<bool> AddTaskFile(List<TaskFile> task);
}