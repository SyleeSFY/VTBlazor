namespace Server.DLL.Models.Entities.Educator;

public class Educator
{
    public int Id { get; set; }
    
    public string Profession { get; set; }
    
    public string FullName { get; set; }
    
    // Ученая степень
    public string AcademicDegree { get; set; }
    
    public EducatorAdditionalInfo EducatorAdditionalInfo { get; set; }

}