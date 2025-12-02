using Microsoft.EntityFrameworkCore;
using Server.DAL.Context.ApplicationDbContext;
using Server.DAL.Models.Entities.Educators;

namespace Server.DAL.Templates;

public class EducatorsBD
{
    private readonly EducatorDbContext _context;

    public EducatorsBD(EducatorDbContext context)
        => _context = context;
    
  private List<Educator> _listEducators = new List<Educator>()
    {
        new Educator()
        {
            FullName = "Зернов Михаил Михайлович",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Полячков Александр Владимирович",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Гетманцев Леонид Юрьевич",
            AcademicDegree = "Старший преподаватель",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Прокуденков Николай Прокофьевич",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Свириденков Константин Иванович",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Свириденкова Марина Александровна",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Денисова Ирина Александровна",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Курылев Владимир Алексеевич",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Тихонов Владимир Александрович",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Фомченков Владимир Петрович",
            AcademicDegree = "Доцент(внутренний совместитель кафедры ВТ)",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Фомин Александр Иванович",
            AcademicDegree = "Доцент",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Андреев Михаил Алексеевич",
            AcademicDegree = "Старший преподаватель",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Федулова Светлана Александровна",
            AcademicDegree = "Старший преподаватель",
            Profession = "Заведующий лабораторией информатизации"
        },
        new Educator()
        {
            FullName = "Федулова Анастасия Сергеевна",
            AcademicDegree = "Старший преподаватель",
            Profession = "Заместитель заведующего кафедрой ВТ по воспитательной работе"
        },
        new Educator()
        {
            FullName = "Жарков Антон Павлович",
            AcademicDegree = "Ассистент",
            Profession = "Заместитель заведующего лабораторией информатизации (технический отдел)"
        },
        new Educator()
        {
            FullName = "Попков Дмитрий Юрьевич",
            AcademicDegree = "Старший преподаватель",
            Profession = "Преподаватель"
        },
        new Educator()
        {
            FullName = "Дубова Наталья Николаевна",
            AcademicDegree = "Сотрудник",
            Profession = "Заместитель заведующего лабораторией информатизации"
        },
        new Educator()
        {
            FullName = "Рыбаков Виктор Алексеевич",
            AcademicDegree = "Сотрудник",
            Profession = "Заведующий лабораторией"
        },
        new Educator()
        {
            FullName = "Ольшевская Ирина Николаевна",
            AcademicDegree = "Сотрудник",
            Profession = "Инженер"
        },
    };
    
    public void AddEducator()
    {
        
    }

    private List<EducatorDiscipline> GetEducatorDisciplines()
    {

        return _context.EducatorDisciplines
            .Include(ed => ed.Discipline) 
            .ToList();
        
    }
}