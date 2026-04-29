namespace Domain.Entities
{
    public class Supplement
    {
        public int Id { get; set; }

        public int WeekPlanId { get; set; }
        public WeekPlan? WeekPlan { get; set; }

        public string Name { get; set; } = "";
        public string Dose { get; set; } = "";
        public string Description { get; set; } = "";
        public bool IsEssential { get; set; }
    }
}