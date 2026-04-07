using Client.Core.Entities.Enums;

namespace Client.Core.Shared
{
    public static class GlobalData
    {
        public static Dictionary<Role, string> GetRusRole = new Dictionary<Role, string>()
        {
            { Role.educator, "Препод." },
            { Role.student, "Студент" },
            { Role.admin, "Админ" }
        };

        public static Dictionary<SolutionStatus, string> GetSolutionStatus = new Dictionary<SolutionStatus, string>()
        {
            { SolutionStatus.InReview, "Проверка" },
            { SolutionStatus.Approved, "Одобрено" },
            { SolutionStatus.Rejected, "Отклонено" }
        };

        public static Dictionary<ValidErrorAuth, string> ValidError = new Dictionary<ValidErrorAuth, string>()
        {
            { ValidErrorAuth.InvalidCredentials, "Ошибка: Неверная почта или пароль!" },
            { ValidErrorAuth.PasswordTooShort, "Ошибка: Пароль не может быть меньше 6 символов!" },
            { ValidErrorAuth.InvalidEmail, "Ошибка: Некорректный формат email!" },
            { ValidErrorAuth.RequiredField, "Ошибка: Заполните все обязательные поля!" },
            { ValidErrorAuth.PasswordTooLong, "Ошибка: Пароль не может быть больше 50 символов!" },
            { ValidErrorAuth.PasswordRequired, "Ошибка: Введите пароль!" },
            { ValidErrorAuth.EmailRequired, "Ошибка: Введите email!" }
        };
    }
}