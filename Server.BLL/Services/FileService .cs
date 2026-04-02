using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Enums;

namespace Server.BLL.Services
{
    public class FileService : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private string _taskDirectoryPath;
        private string _solutionDirectoryPath;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository ?? throw new ArgumentNullException(nameof(fileRepository));
            InitDirectory();
        }

        private void InitDirectory()
        {
            var currentPath = Directory.GetCurrentDirectory();

            var projectPath = Path.GetFullPath(Path.Combine(currentPath, "..", "Server.DAL"));

            if (!Directory.Exists(projectPath))
                projectPath = currentPath;

            _taskDirectoryPath = Path.Combine(projectPath, "File", "Tasks");
            _solutionDirectoryPath = Path.Combine(projectPath, "File", "Solutions");

            Directory.CreateDirectory(_taskDirectoryPath);
            Directory.CreateDirectory(_solutionDirectoryPath);
        }

        public async Task<byte[]> GetFile(int fileId, FileType fileType)
        {
            var fileBD = fileType == FileType.Task
                ? await GetTaskFileFromBD(fileId)
                : await GetSolutionFileFromBD(fileId);

            if (fileBD == null || string.IsNullOrEmpty(fileBD.PhysicalPath))
                return Array.Empty<byte>();

            var file = await GetFileFromDisk(fileBD.PhysicalPath, fileType);
            return file.Length > 0 ? file : Array.Empty<byte>();
        }

        public async Task<string> SaveFileToDisk(byte[] fileBytes, string fileName, string disciplineName, FileType fileType)
        {
            var safeDisciplineName = string.Join("_", disciplineName.Split(Path.GetInvalidFileNameChars()));
            var taskFolder = fileType == FileType.Task
                ? Path.Combine(_taskDirectoryPath, safeDisciplineName)
                : Path.Combine(_solutionDirectoryPath, safeDisciplineName);

            Directory.CreateDirectory(taskFolder);

            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            var fullPath = Path.Combine(taskFolder, uniqueFileName);

            await File.WriteAllBytesAsync(fullPath, fileBytes);

            var relativePath = fileType == FileType.Task
                ? Path.Combine("File", "Tasks", safeDisciplineName, uniqueFileName)
                : Path.Combine("File", "Solutions", safeDisciplineName, uniqueFileName);

            return relativePath.Replace("\\", "/");
        }

        private async Task<byte[]> GetFileFromDisk(string physicalPath, FileType fileType)
        {
            var fullPath = GetFullPath(physicalPath, fileType);

            if (!File.Exists(fullPath))
                throw new FileNotFoundException($"File not found: {fullPath}");

            return await File.ReadAllBytesAsync(fullPath);
        }

        private string GetFullPath(string physicalPath, FileType fileType)
        {
            var relativePath = fileType == FileType.Task
                ? physicalPath.Replace("File/Tasks/", "")
                : physicalPath.Replace("File/Solutions/", "");

            var basePath = fileType == FileType.Task ? _taskDirectoryPath : _solutionDirectoryPath;
            return Path.Combine(basePath, relativePath);
        }

        public async Task DeleteFileFromDisk(string physicalPath, FileType fileType)
        {
            if (string.IsNullOrEmpty(physicalPath))
                return;

            var fullPath = GetFullPath(physicalPath, fileType);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }

        public async Task<TaskFile> GetTaskFileFromBD(int fileId)
        {
            return await _fileRepository.GetTaskFile(fileId) ?? new TaskFile();
        }

        public async Task<TaskFile> GetSolutionFileFromBD(int fileId)
        {
            return await _fileRepository.GetSolutionFile(fileId) ?? new TaskFile();
        }
    }
}