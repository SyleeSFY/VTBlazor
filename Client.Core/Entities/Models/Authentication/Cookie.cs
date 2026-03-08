namespace Client.Core.Entities.Models.Authentication
{
    public class Cookie
    {
        public string Email { get; set; }
        public string JWT { get; set; }
        public DateTime ExpiredAt { get; set; }

    }
}
