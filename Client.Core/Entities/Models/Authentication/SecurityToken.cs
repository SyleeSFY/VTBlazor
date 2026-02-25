namespace Client.Core.Entities.Models.Authentication
{
    public class SecurityToken
    {
        public string Email { get; set; }
        public string AccessToken { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
