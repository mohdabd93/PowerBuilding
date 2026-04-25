using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class WorkoutDay
    {
        public int Id { get; set; }
        public int DayNumber { get; set; }          // 1, 2, 3...
        public string DayName { get; set; } = "";   // chest main, legs main, upper ....
        public string Focus { get; set; } = "";   // "Chest Primary | Push"
        public bool IsRestDay { get; set; }  // rest, workout
        public List<Exercise> exercises { get; set; } = new(); // list of Exercises

        //stringht Exercises
        public List<Exercise> Strength=> 
            exercises.Where(str=>str.Type==Enum.ExerciseType.Strength).ToList();
        //Mass Exercises 
        public List<Exercise> Hypertrophy =>
            exercises.Where(hyp=>hyp.Type==Enum.ExerciseType.Hypertrophy).ToList();
        //Definition Exercises
        public List<Exercise> Definition =>
              exercises.Where(def=>def.Type==Enum.ExerciseType.Definition).ToList();

    }
}
