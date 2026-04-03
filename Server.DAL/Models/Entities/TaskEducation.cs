using Server.DAL.Models.Entities.Education;
using Server.DAL.Models.Entities.Educators;
using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities;

public class TaskEducation
{
    public int Id { get; set; }
    public int DiciplineId { get; set; }
    public int EducatorId { get; set; }
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;
    public List<Group> Groups { get; set; } = new List<Group>();
    public DateTime CreatedAt { get; set; }
    public List<TaskFile>? Files { get; set; }
    public Educator Educator { get; set; }
    public Discipline Dicipline { get; set; }
    public List<StudentSolution>? StudentSolutions { get; set; } = new List<StudentSolution>();
}

public class TaskFile
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public string PhysicalPath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public DateTime UploadedAt { get; set; }
    public string FileType { get; set; } = string.Empty;
    [JsonIgnore]
    public TaskEducation Task { get; set; }
}