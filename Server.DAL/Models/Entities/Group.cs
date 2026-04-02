using Server.DAL.Models.Entities.Users;
using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities;

public class Group
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    [JsonIgnore]
    public ICollection<Student> Students { get; set; }
    [JsonIgnore]
    public ICollection<TaskEducation> TaskEducations { get; set; }
}