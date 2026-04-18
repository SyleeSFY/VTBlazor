namespace Client.Core.Entities.Models.DTO
{
    public class FileInChatDTO
    {
        public string FileName { get; set; } = string.Empty;
        public string ContentBase64 { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
    }
}