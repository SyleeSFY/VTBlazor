using System.Net.Http.Json;

namespace Client.Core.Pages;

public partial class Contacts
{
    private Educator? educator;
    private bool isLoading = false;
    private string? errorMessage = null;
    
    private async Task GetEducator()
    {
        isLoading = true;
        errorMessage = null;
        StateHasChanged();
        
        try
        {
            educator = await Http.GetFromJsonAsync<Educator>("api/educators/1");
        }
        catch (Exception ex)
        {
            errorMessage = $"Не удалось загрузить данные: {ex.Message}";
        }
        finally
        {
            isLoading = false;
            StateHasChanged();
        }
    }
    
    public class Educator
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? AcademicDegree { get; set; }
        public EducatorAdditionalInfo EducatorAdditionalInfo { get; set; }

    }
    public class EducatorAdditionalInfo
    {
        public int Id { get; set; }
    
        public int EducatorId { get; set; }
    
        //Уровень образования
        public string EducationLevel { get; set; } 
    
        //Ученое звание
        public string AcademicTitle { get; set; }
    
        //Специальность или направление подготовки 
        public string SpecialtyOrFieldOfStudy { get; set; }
    
        //Квалификация
        public string Qualification { get; set; }
        
        //дисциплины
        public List<EducatorDiscipline> EducatorDisciplines { get; set; }
    
        //Доп инфа
        public string AdditionalInfo { get; set; }

        public List<byte> Image { get; set; }
    }
    public class EducatorDiscipline
    {
        public int Id { get; set; }
        public int EducatorAdditionalInfoId { get; set; } 
        public int DisciplineId { get; set; }
        public Discipline Discipline { get; set; }
    }
    
    public class Discipline
    {
        public int Id { get; set; }
        public string NameDiscipline { get; set; }
    }

}