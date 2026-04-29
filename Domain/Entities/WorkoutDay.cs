using Domain.Enum;

namespace Domain.Entities
{
    public class WorkoutDay
    {
        public int Id { get; set; }

        public int WeekPlanId { get; set; }
        public WeekPlan? WeekPlan { get; set; }

        public int DayNumber { get; set; }
      //  public string DayName { get; set; } = "";
      //  public string Focus { get; set; } = "";
        public TrainingSplitType DayName { get; set; }
        public TargetMuscle Focus { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public bool IsRestDay { get; set; }

        public List<Exercise> exercises { get; set; } = new();

   
    }
}