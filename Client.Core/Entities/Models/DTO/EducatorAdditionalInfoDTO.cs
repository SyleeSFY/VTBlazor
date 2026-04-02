namespace Client.Core.Entities.Models.DTO
{
    public class EducatorAdditionalInfoDTO
    {
        public string EducationLevel { get; set; }

        public string AcademicTitle { get; set; }

        public string SpecialtyOrFieldOfStudy { get; set; }

        public string Qualification { get; set; }

        public string AdditionalInfo { get; set; }
        public string? Image { get; set; }
        public List<EducatorDisciplineDTO> EducatorDisciplines { get; set; }
    }
}
