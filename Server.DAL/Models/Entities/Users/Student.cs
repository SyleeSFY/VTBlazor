using Server.DAL.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.DAL.Models.Entities.Users
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [MaxLength(20)]
        public string StudentId { get; set; }
        public string? GroupId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
    }
}
