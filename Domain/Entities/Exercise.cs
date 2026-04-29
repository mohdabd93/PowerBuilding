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
        public int WorkoutDayId { get; set; }
        public WorkoutDay WorkoutDay { get; set; }
        public string Name { get; set; } = "";   // "Bench Press"
        public string NameAr { get; set; } = "";   //  "Bench Press"
        public string Note { get; set; } = "";   // notes
        public int Sets { get; set; }          //sets count
        public string Reps { get; set; } = "";   // reps cound
        public string Rest { get; set; } = "";   //rest between sets
        public ExerciseType Type { get; set; }

        public MuscleGroup Muscle { get; set; }
        public ExerciseVariation Variation { get; set; }

        //should makes it better when use max/min in order to use it later for Exercise Logs   
        //if (log.Reps == exercise.MaxReps) icrease weight
        //if (log.Reps < exercise.MinReps) decrease wieht

        public int MinReps { get; set; }
        public int MaxReps { get; set; }
    }
}
