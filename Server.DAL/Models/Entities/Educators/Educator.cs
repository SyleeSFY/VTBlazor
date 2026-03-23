using Server.DAL.Models.Entities.Users;
using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities.Educators;

public class Educator
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Profession { get; set; }
    public string FullName => User != null
          ? $"{User.LastName} {User.FirstName} {User.MiddleName}".Trim()
          : string.Empty;
    public string AcademicDegree { get; set; }
  
    public EducatorAdditionalInfo EducatorAdditionalInfo { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}