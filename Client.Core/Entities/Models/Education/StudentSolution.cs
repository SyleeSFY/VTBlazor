using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.User;
using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.Education
{
    /// <summary>
    /// Решение студента
    /// Ответ на Task
    /// </summary>
    public class StudentSolution
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public int StudentId { get; set; }
        public string SolutionText { get; set; } = string.Empty;
        public List<SolutionFile>? SolutionFiles { get; set; }
        public SolutionStatus Status { get; set; } = SolutionStatus.InReview;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public SolutionChat? SolutionChat { get; set; }

        // Навигационные свойства
        [JsonIgnore]
        public TaskEducation Task { get; set; }

        [JsonIgnore]
        public Student Student { get; set; }
    }
}