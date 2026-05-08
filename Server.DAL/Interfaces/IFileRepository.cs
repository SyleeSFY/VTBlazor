using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Education;

namespace Server.DAL.Interfaces;

public interface IFileRepository
{
    Task<bool> AddTaskFile(List<TaskFile> task);
    Task<bool> AddSolutionFile(List<SolutionFile> files);
    Task<TaskFile> GetTaskFile(int fileId);
    Task<SolutionFile> GetSolutionFile(int fileId);
    Task<bool> AddMessageFile(List<FileInChat> files);
    Task<FileInChat> GetMessageFile(int fileId);
}