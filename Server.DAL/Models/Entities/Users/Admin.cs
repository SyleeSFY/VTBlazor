using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.DAL.Models.Entities.Users
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
