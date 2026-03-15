namespace Server.DAL.Models.DTO
{
    public class DisciplineDTO
    {
        public string NameDiscipline { get; set; }
        public int Course { get; set; }
        public bool isMagistracy { get; set; } = false;
        public TrainedGroupDTO? Group { get; set; }
    }
}