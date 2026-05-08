using Client.Core.Entities.Enums;

namespace Client.Core.Entities.Models.DTO
{
    public class MessageInChatDTO
    {
        public int ChatId { get; set; }
        public int SenderId { get; set; }
        public Role SenderRole { get; set; }
        public string MessageText { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public List<FileInChatDTO>? Files { get; set; }
    }
}
