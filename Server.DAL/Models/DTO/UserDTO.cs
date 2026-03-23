using Server.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Server.DAL.Models.DTO
{
    public class UserDTO
    {
        [Required]
        [EmailAddress]
        [MaxLength(256)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [MaxLength(100)]
        public string? MiddleName { get; set; }

        [Required]
        public Role Role { get; set; }
        public EducatorDTO? Educator { get; set; }
        public StudentDTO? Student { get; set; }
        public AdminDTO? Administrator { get; set; }
    }
}