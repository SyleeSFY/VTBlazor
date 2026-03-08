namespace Client.Core.Entities.Models.Authentication
{
    public class AuthResponce
    {
        public string JwtToken { get; set; }
        public bool Success { get; set; } = false;
    }
}
