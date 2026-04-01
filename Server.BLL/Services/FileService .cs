using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;

namespace Server.BLL.Services
{
    public class FileService : IFileService
    {
        private IFileRepository _fileRepository;
        
        private readonly string _Path;
        
        private string _educatorDirectoryPath;
        private string _studentDirectoryPath;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
            InitDirectory();

        }

        public void InitDirectory()
        {
            var _currentPath = Directory.GetCurrentDirectory();
            
            var projectPath = Path.GetFullPath(Path.Combine(_currentPath, "..", "Server.DAL"));

            if (!Directory.Exists(projectPath))
                projectPath = _currentPath;

            _educatorDirectoryPath = Path.Combine(projectPath, "Tasks/educatorFile");
            _studentDirectoryPath = Path.Combine(projectPath, "Tasks/studentFile");

                Directory.CreateDirectory(_educatorDirectoryPath);
                Directory.CreateDirectory(_studentDirectoryPath);
        }

        public async Task<TaskFile> GetFileFromBD(int fileId)
        {
            try
            {
                var file = await _educatorRepository.GetTaskFile(fileId);
                if (file != null)
                    return file;
                return new TaskFile();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<byte[]> GetFile(int fileId)
        {
            var file = await GetFileFromBD(fileId);
            var qwe = await _fileService.GetFileFromDisk(file.PhysicalPath);
            if (qwe is not null && qwe.Length > 0)
                return qwe;
            return new byte[0];
        }

        public async Task<string> SaveFileToDisk(byte[] fileBytes, string fileName, int taskId, string disciplineName)
        {
            var safeDisciplineName = string.Join("_", disciplineName.Split(Path.GetInvalidFileNameChars()));
            var taskFolder = Path.Combine(_Path, safeDisciplineName);

            if (!Directory.Exists(taskFolder))
                Directory.CreateDirectory(taskFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var fullPath = Path.Combine(taskFolder, uniqueFileName);

            await File.WriteAllBytesAsync(fullPath, fileBytes);
            return Path.Combine("Tasks", safeDisciplineName, uniqueFileName).Replace("\\", "/");
        }

        public async Task<byte[]> GetFileFromDisk(string physicalPath)
        {
            var pathReolase = physicalPath.Replace("Tasks", "");
            var qwe = $"{_Path}{pathReolase}";
            var fullPath = Path.Combine(_Path, physicalPath.Replace("Tasks", ""));

            if (!File.Exists(qwe))
                throw new FileNotFoundException($"File not found: {fullPath}");

            return await File.ReadAllBytesAsync(qwe);
        }

        public async Task DeleteFileFromDisk(string physicalPath)
        {
            var fullPath = Path.Combine(_Path, physicalPath.Replace("Tasks", ""));

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
    }
}