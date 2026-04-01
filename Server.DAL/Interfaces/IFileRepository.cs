using Server.DAL.Models.Entities;

namespace Server.DAL.Interfaces;

public interface IFileRepository
{
    Task<bool> AddTaskFile(List<TaskFile> task);
    Task<TaskFile> GetTaskFile(int fileId);
    Task<TaskFile> GetSolutionFile(int fileId);
}