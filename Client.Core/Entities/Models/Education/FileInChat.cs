using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.Education
{
    public class FileInChat
    {
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string PhysicalPath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
        
        [JsonIgnore]
        public MessageInChat Message { get; set; }
    }
}