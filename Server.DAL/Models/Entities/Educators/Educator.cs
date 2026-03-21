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

    // Ученая степень
    public string AcademicDegree { get; set; }
    
    public EducatorAdditionalInfo EducatorAdditionalInfo { get; set; }
    [JsonIgnore]
    public User? User { get; set; }

    public Educator()
    {
        EducatorAdditionalInfo = new EducatorAdditionalInfo();
    }
}