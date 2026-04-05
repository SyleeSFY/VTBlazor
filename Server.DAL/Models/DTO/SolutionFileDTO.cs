namespace Server.DAL.Models.DTO
{
    public class SolutionFileDTO
    {
        public string FileName { get; set; } = string.Empty;
        public string OriginalFileName { get; set; } = string.Empty;
        public long FileSize { get; set; }
        public string FileType { get; set; } = string.Empty;
        public DateTime UploadedAt { get; set; }
        public string ContentBase64 { get; set; } = string.Empty;
    }
}
