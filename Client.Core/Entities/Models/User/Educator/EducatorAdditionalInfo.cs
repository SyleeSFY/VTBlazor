namespace Client.Core.Entities.Models.User.Educator;

/// <summary>
/// EducationLevel - Уровень образования
/// AcademicTitle - Ученое звание
/// SpecialtyOrFieldOfStudy - Специальность или направление подготовки 
/// Qualification - Квалификация
/// EducatorDisciplines - дисциплины
/// AdditionalInfo - Доп инфа
/// </summary>
public class EducatorAdditionalInfo
{
    public int Id { get; set; }
    
    public int EducatorId { get; set; }
    
    public string EducationLevel { get; set; } 
    
    public string AcademicTitle { get; set; }
    
    public string SpecialtyOrFieldOfStudy { get; set; }
    
    public string Qualification { get; set; }
    
    public string AdditionalInfo { get; set; }
    public string? Image { get; set; }
    public List<EducatorDiscipline> EducatorDisciplines { get; set; }
    
    public string GetIMG()
    {
        if (!string.IsNullOrEmpty(Image))
            return $"data:image/jpeg;base64,{Image}";
        return String.Empty;
    }
}