using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Client.Core.Entities.Models.User
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
    }
}
