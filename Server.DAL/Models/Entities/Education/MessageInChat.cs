using Server.DAL.Models.Enums;
using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities.Education
{
    public class MessageInChat
    {
        public int Id { get; set; }
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public Role SenderRole { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public List<FileInChat>? Files { get; set; }

        [JsonIgnore]
        public SolutionChat Chat { get; set; }
    }
}
