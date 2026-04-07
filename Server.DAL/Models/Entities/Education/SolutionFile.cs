using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities.Education
{
    /// <summary>
    /// Класс для файлов решения
    /// </summary>
    public class SolutionFile
    {
        public int Id { get; set; }
        public int SolutionId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public string PhysicalPath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }

        [JsonIgnore]
        public StudentSolution Solution { get; set; }
    }
}
