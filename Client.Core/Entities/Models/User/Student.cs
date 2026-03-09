using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
