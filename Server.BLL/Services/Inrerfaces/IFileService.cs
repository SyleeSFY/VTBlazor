using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.BLL.Services.Inrerfaces
{
    public interface IFileService
    {
        Task<string> SaveFileToDisk(byte[] fileBytes, string fileName, int taskId, string name);
        Task<byte[]> GetFileFromDisk(string physicalPath);
        Task DeleteFileFromDisk(string physicalPath);
    }
}
