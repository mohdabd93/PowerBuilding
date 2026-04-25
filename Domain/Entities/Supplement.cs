using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Supplement
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";  //whey protine, creatine....
        public string Dose { get; set; } = "";   // 5 gm a day, 10 gmm working out day
        public string Description { get; set; } = "";   // before sleeping, before working out
        public bool IsEssential { get; set; }
    }
}
