namespace Server.DLL.Models.Entities.Educator;

/// <summary>
/// Класс с дополнительной информацией о преподавателе
/// </summary>
public class EducatorAdditionalInfo
{
    public int Id { get; set; }
    
    public int EducatorId { get; set; }
    
    //Уровень образования
    public string EducationLevel { get; set; } 
    
    //Ученое звание
    public string AcademicTitle { get; set; }
    
    //Специальность или направление подготовки 
    public string SpecialtyOrFieldOfStudy { get; set; }
    
    //Квалификация
    public string Qualification { get; set; }
        
    //дисциплины
    public List<EducatorDiscipline> EducatorDisciplines { get; set; }
    
    //Доп инфа
    public string AdditionalInfo { get; set; }

    public List<byte> Image { get; set; }
}


