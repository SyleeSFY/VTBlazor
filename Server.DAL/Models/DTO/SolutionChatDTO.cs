namespace Server.DAL.Models.DTO
{
    public class SolutionChatDTO
    {
        public DateTime CreatedAt { get; set; }
        public List<MessageInChatDTO> Messages { get; set; } = new List<MessageInChatDTO>();
    }
}
