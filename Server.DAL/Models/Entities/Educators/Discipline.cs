namespace Server.DAL.Models.Entities.Educators;

public class Discipline
{
    public int Id { get; set; }
    public string NameDiscipline { get; set; }
    public int Course { get; set; }
    public bool isMagistracy { get; set; } = false;
    public TrainedGroup? Group { get; set; }
}