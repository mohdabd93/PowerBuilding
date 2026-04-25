using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";   // brekfast, lunch, snak
        public string Time { get; set; } = "";   //7-8 AM
        public int Calories { get; set; }          // 700
        public int Protein { get; set; }          // 40 gm
        public List<string> FoodItems { get; set; } = new(); // food list
        public string? Tip { get; set; }
    }
}
