using Server.DAL.Models.Entities.Educators;

namespace Server.DAL.Models.Entities;

public class TaskEducation
{
    public int Id { get; set; }
    public int DiciplineId { get; set; }
    public int EducatorId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;
    public List<Group> Groups { get; set; } = new List<Group>();
    public Educator Educator { get; set; }
    public Discipline Dicipline { get; set; }
    public List<TaskFile> Files { get; set; } = new List<TaskFile>();
}

public class TaskFile
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string PhysicalPath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; }
    
    public TaskEducation Task { get; set; }
}