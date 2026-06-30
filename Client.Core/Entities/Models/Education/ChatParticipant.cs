using Client.Core.Entities.Enums;
using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.Education {
    public class ChatParticipant {
        public int Id { get; set; }
        public int SolutionChatId { get; set; }
        public int SenderId { get; set; }
        public bool HasUnreadMessages { get; set; }

        [JsonIgnore]
        public SolutionChat SolutionChat { get; set; }
    }
}
