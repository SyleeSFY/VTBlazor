using Client.Core.Entities.Models.User.Educator;

namespace Client.Core.Entities.Models.DTO
{
    public class EducatorDTO
    {
        public string Profession { get; set; }
        public required string FullName { get; set; } = string.Empty;
        public string? AcademicDegree { get; set; }
        public EducatorAdditionalInfoDTO EducatorAdditionalInfo { get; set; }
    }
}
