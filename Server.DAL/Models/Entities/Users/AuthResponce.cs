namespace Server.DAL.Models.Entities.Users
{
    public class AuthResponce
    {
        public string JwtToken { get; set; }
        public bool Success { get; set; } = false;
    }
}