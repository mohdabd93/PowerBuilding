using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExerciseLog
    {
        public int Id { get; set; }
        public int ExerciseId { get; set; }
        public Exercise? Exercise { get; set; }
        public int Reps { get; set; }
        public decimal Weight { get; set; }  // current
        public string Notes { get; set; } = "";
        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}
