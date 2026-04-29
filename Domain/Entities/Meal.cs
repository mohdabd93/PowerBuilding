namespace Domain.Entities
{
    public class Meal
    {
        public int Id { get; set; }

        public int NutritionPlanId { get; set; }
        public NutritionPlan? NutritionPlan { get; set; }
     //   public int WeekPlanId { get; set; }
      //  public WeekPlan? WeekPlan { get; set; }
        public string Name { get; set; } = "";
        public string Time { get; set; } = "";
        public int Calories { get; set; }
        public int Protein { get; set; }

        public List<string> FoodItems { get; set; } = new();
        public string? Tip { get; set; }
    }
}