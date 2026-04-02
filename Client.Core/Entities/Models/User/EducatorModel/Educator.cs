namespace Client.Core.Entities.Models.User.EducatorModel;

public class Educator
{
    public int Id { get; set; }
    public string Profession { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string? AcademicDegree { get; set; }
    public EducatorAdditionalInfo EducatorAdditionalInfo { get; set; }
}
