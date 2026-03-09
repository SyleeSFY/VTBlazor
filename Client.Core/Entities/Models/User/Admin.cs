using System.ComponentModel.DataAnnotations;

namespace Client.Core.Entities.Models.User
{
    public class Admin
    {
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }
        [MaxLength(100)]
        public string Position { get; set; } = string.Empty;

        public User User { get; set; }
    }
}
