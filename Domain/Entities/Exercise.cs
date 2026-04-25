using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Exercise
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";   // "Bench Press"
        public string NameAr { get; set; } = "";   //  "Bench Press"
        public string Note { get; set; } = "";   // notes
        public int Sets { get; set; }          //sets count
        public string Reps { get; set; } = "";   // reps cound
        public string Rest { get; set; } = "";   //rest between sets
        public ExerciseType Type { get; set; }
    }
}
