using Client.Core.Entities.Models.User;

namespace Client.Core.Entities.Models.DTO;

public class TaskEducationDTO
{
    public string TaskName { get; set; } = string.Empty;
    public string TaskDescription { get; set; } = string.Empty;
    public int EducatorId { get; set; }
    public int DiciplineId { get; set; }
    public List<int> GroupId { get; set; } = new List<int>();
    public List<TaskFileDTO>? Files { get; set; } = new List<TaskFileDTO>();
}

public class TaskFileDTO
{
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentBase64 { get; set; } = string.Empty; // Для Wasm
}