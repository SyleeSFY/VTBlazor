using Server.BLL.Services.Inrerfaces;
using Server.DAL.Interfaces;
using Server.DAL.Models.DTO;
using Server.DAL.Models.Entities;
using Server.DAL.Models.Entities.Educators;
using Server.DAL.Models.Enums;

namespace Server.BLL.Services;

public class EducatorService : IEducatorService
{
    private readonly IEducatorRepository _educatorRepository;
    private readonly IDiciplineService _diciplineService;
    private readonly IFileService _fileService;
    private readonly IFileRepository _fileRepository;

    public EducatorService(IEducatorRepository educatorRepository, IDiciplineService diciplineService, IFileService fileService, IFileRepository fileRepository)
    { 
        _educatorRepository = educatorRepository;
        _diciplineService = diciplineService;
        _fileService = fileService;
        _fileRepository = fileRepository;
    }
    
    /// <summary>
    /// Получение единичного преподавателя,
    /// со всеми связанными сущностями
    /// </summary>
    /// <param name="id"></param>
    public async Task<Educator> GetByIdAsync(int id)
    {
        return await _educatorRepository.GetByIdAsync(id);
    }

    public async Task<Educator> GetSimpleByUserId(int userId)
    {
        return await _educatorRepository.GetSimpleByUserId(userId);
    }
    
    /// <summary>
    /// Получение educator по id(упрощенный вариант)
    /// </summary>
    /// <param name="id"></param>
    public async Task<Educator> GetByIdSimpleAsync(int id)
    {
        return await _educatorRepository.GetByIdAsync(id);
    }

    public async Task<List<Educator>> GetEducatorsAsync()
    {
        var educators = await _educatorRepository.GetEducatorsAsync();
        if (educators.Count != 0)
            return educators;
        return new List<Educator>();
    }

    /// <summary>
    /// Упрощенный список educators
    /// </summary>
    /// <param name="id"></param>
    public async Task<List<Educator>> GetEducatorsSimpleAsync()
    {
        try
        {
            var educators = await _educatorRepository.GetEducatorsSimpleAsync();
            if (educators.Count != 0)
                return educators;
            return new List<Educator>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public async Task AddEducator(Educator educator)
    {
        await _educatorRepository.AddEducator(educator);
    }
    
    public async Task<List<TaskEducation>> GetTasksEducatorByIdSimple(int EducatorId)
    {
        try
        {
            var groups = await _educatorRepository.GetTasksEducatorByIdSimple(EducatorId);
            if (groups.Count != 0)
                return groups;
            return new List<TaskEducation>();
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<TaskEducation> GetTasksEducatorById(int id)
    {
        try
        {
            var task = await _educatorRepository.GetTasksEducatorById(id);
            if (task != null)
                return task;
            return new TaskEducation();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<TaskEducation>> GetEducatorTaskByGroup(int id)
    {
        try
        {
            var task = await _educatorRepository.GetTasksEducatorByGroup(id);
            if (task.Count is not 0 && task.Any() )
                return task;
            return new List<TaskEducation>();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<List<Group>> GetGroupsAsync()
    {
        try
        {
            var groups = await _educatorRepository.GetGroupsAsync();
            if (groups.Count != 0)
                return groups;
            return new List<Group>();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<bool> AddTask(TaskEducationDTO taskDto)
    {
        var compFiles = new List<TaskFile>();
        var groups = await GetGroupsAsync();
        var dicipline = await _diciplineService.GetDiciplineAsync(taskDto.DiciplineId);

        var task = new TaskEducation
        {
            TaskName = taskDto.TaskName,
            TaskDescription = taskDto.TaskDescription,
            EducatorId = taskDto.EducatorId,
            DiciplineId = taskDto.DiciplineId,
            CreatedAt = DateTime.UtcNow,
            Groups = groups.Where(x => taskDto.GroupId.Contains(x.Id)).ToList(),
        };

        var taskId = await _educatorRepository.AddTask(task);
        if (taskId is not 0 && taskDto.Files.Any() && taskDto.Files != null)
        {
            foreach (var file in taskDto.Files)
            {
                var bytes = Convert.FromBase64String(file.ContentBase64);
                var physicalPath = await _fileService.SaveFileToDisk(bytes, file.FileName, dicipline.NameDiscipline, FileType.Task);
                compFiles.Add(new TaskFile()
                {
                    TaskId = taskId,
                    FileName = file.FileName,
                    PhysicalPath = physicalPath,
                    FileSize = file.FileSize,
                    UploadedAt = DateTime.UtcNow,
                    FileType = file.FileType
                });
            }

            if (compFiles.Any())
                await _fileRepository.AddTaskFile(compFiles);
        }

        return true;
    }
}