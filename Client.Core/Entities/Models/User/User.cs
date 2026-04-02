using System.ComponentModel.DataAnnotations;
using Client.Core.Entities.Enums;
using Client.Core.Entities.Models.User.EducatorModel;

namespace Client.Core.Entities.Models.User
{
    public class User
    {
        public int Id { get; set; }

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
        public bool IsActive { get; set; } = true;

        public Educator? Educator { get; set; }
        public Student? Student { get; set; }
        public Admin? Administrator { get; set; }
    }
}
