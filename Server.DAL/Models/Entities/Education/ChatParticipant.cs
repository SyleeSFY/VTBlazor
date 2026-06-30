using Server.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.DAL.Models.Entities.Education {
    public class ChatParticipant {
        public int Id { get; set; }
        public int SolutionChatId { get; set; }
        public int SenderId { get; set; }
        public bool HasUnreadMessages { get; set; }

        [JsonIgnore]
        public SolutionChat SolutionChat { get; set; }
    }
}
