using Server.DAL.Models.Entities;
using Server.DAL.Models.Enums;

namespace Server.BLL.Services.Inrerfaces
{
    public interface IFileService
    {
        Task<string> SaveFileToDisk(byte[] fileBytes, string fileName, string disciplineName, FileType fileType);
        Task<byte[]> GetFile(int fileId, FileType fileType);
        Task DeleteFileFromDisk(string physicalPath, FileType fileType);
        Task<TaskFile> GetTaskFileFromBD(int fileId);
        Task<TaskFile> GetSolutionFileFromBD(int fileId);
    }
}
