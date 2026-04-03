using Server.DAL.Models.Entities.Education;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Server.DAL.Models.Entities.Users
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }

        [MaxLength(20)]
        public string StudentCard { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public Group Group { get; set; }
        public List<StudentSolution>? Solutions { get; set; }
    }
}