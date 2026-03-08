using System.ComponentModel.DataAnnotations;

namespace Client.Core.Entities.Models.Authentication
{
    public class AuthorizationData
    {
        [Required]
        [StringLength(50, ErrorMessage = "Long")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
