using Server.DAL.Models.Enums;

namespace Server.DAL.Models.DTO
{
    public class SolutionStudentDTO
    {
        public int TaskId { get; set; }
        public int StudentId { get; set; }
        public int? SolutionChatId { get; set; }
        public string SolutionText { get; set; } = string.Empty;
        public List<SolutionFileDTO>? SolutionFiles { get; set; }
        public SolutionStatus Status { get; set; } = SolutionStatus.InReview;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public SolutionChatDTO? SolutionChat { get; set; }
    }
}
