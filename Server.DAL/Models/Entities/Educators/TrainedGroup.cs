using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Server.DAL.Models.Entities.Educators
{

    public class TrainedGroup
    {
        public int Id { get; set; }
        public bool isAS { get; set; } = false;
        public bool isPO { get; set; } = false;
        public bool isVM { get; set; } = false;
        public int DisciplineId { get; set; }
        [JsonIgnore]
        public Discipline Discipline { get; set; }
    }
}

