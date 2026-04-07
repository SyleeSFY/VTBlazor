using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.Education
{
    public class SolutionChat
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<MessageInChat> Messages { get; set; } = new List<MessageInChat>();

        [JsonIgnore]
        public StudentSolution Solution { get; set; }
    }
}
