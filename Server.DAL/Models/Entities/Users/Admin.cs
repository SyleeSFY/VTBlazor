using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities.Users
{
    public class Admin
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;
        [JsonIgnore]
        public User User { get; set; }
    }
}