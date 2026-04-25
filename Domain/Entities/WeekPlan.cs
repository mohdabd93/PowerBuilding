using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WeekPlan
    {
        public int Id { get; set; }
        public string Label { get; set; } = "";   // Week 1,2...
        public string Note { get; set; } = "";   //level tips or notes

        public List<WorkoutDay> Days { get; set; } = new();
    }
}
