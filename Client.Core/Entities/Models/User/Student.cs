using System.ComponentModel.DataAnnotations;

namespace Client.Core.Entities.Models.User
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [MaxLength(20)]
        public string StudentId { get; set; }
        public string? GroupId { get; set; }
        public User User { get; set; }
    }
}
