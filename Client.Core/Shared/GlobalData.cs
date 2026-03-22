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
    }
}