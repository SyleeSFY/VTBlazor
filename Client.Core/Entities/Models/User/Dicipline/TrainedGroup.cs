using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.User.Dicipline
{
    public class TrainedGroup
    {
        public int Id { get; set; }
        public bool isAS { get; set; } = false;
        public bool isPO { get; set; } = false;
        public bool isVM { get; set; } = false;
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
}
