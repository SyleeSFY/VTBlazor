namespace Client.Core.Shared.Models;

public class Educator
{
    public required int Id { get; set; }
    public string Profession { get; set; }
    public required string FullName { get; set; } = string.Empty;
    public string? AcademicDegree { get; set; }
    public EducatorAdditionalInfo EducatorAdditionalInfo { get; set; }
}