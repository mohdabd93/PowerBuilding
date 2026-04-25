using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class NutritionPlan
    {
        public int Id { get; set; }
        public int TotalCalories { get; set; }   // 2900
        public int TotalProtein { get; set; }   // 185 gm
        public int TotalCarbs { get; set; }   // 330 gm
        public int TotalFat { get; set; }   // 85 gm

        public List<Meal> Meals { get; set; } = new();
    }
}
