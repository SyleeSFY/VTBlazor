namespace Client.Core.Entities.Models.DTO
{
    public class FileInChatDTO
    {
        public int MessageId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string PhysicalPath { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
    }
}