using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.DTO
{
    public class SolutionChatDTO
    {
        public DateTime CreatedAt { get; set; }
        public List<MessageInChatDTO> Messages { get; set; } = new List<MessageInChatDTO>();
    }
}
