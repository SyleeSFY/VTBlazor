using Client.Core.Entities.Models.User.Dicipline;

namespace Client.Core.Entities.Models.User.Educator;

public class EducatorDiscipline
{
    public int Id { get; set; }
    public int EducatorAdditionalInfoId { get; set; } 
    public int DisciplineId { get; set; }
    public Discipline Discipline { get; set; }
}