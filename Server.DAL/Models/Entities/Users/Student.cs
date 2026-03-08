using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Server.DAL.Models.Entities.Users;
using System.Linq;
using System.Text;
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

        public User User { get; set; }
    }
}
