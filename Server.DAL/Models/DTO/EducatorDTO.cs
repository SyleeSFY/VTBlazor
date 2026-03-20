namespace Server.DAL.Models.DTO
{
    public class EducatorDTO
    {
        public string Profession { get; set; }
        public required string FullName { get; set; } = string.Empty;
        public string? AcademicDegree { get; set; }
        public EducatorAdditionalInfoDTO EducatorAdditionalInfo { get; set; }
    }
}
