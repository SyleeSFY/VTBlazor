using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;

namespace Server.DAL.Interfaces;

public interface IFileRepository
{
    Task<bool> AddTaskFile(List<TaskFile> task);
    Task<bool> AddSolutionFile(List<SolutionFile> files);
    Task<TaskFile> GetTaskFile(int fileId);
    Task<TaskFile> GetSolutionFile(int fileId);
}