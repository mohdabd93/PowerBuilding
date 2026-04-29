namespace Domain.Entities
{
    public class NutritionPlan
    {
        public int Id { get; set; }

        public int WeekPlanId { get; set; }
        public WeekPlan? WeekPlan { get; set; }

        public int TotalCalories { get; set; }
        public int TotalProtein { get; set; }
        public int TotalCarbs { get; set; }
        public int TotalFat { get; set; }

        public List<Meal> Meals { get; set; } = new();
    }
}