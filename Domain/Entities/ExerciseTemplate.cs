using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExerciseTemplate
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public TargetMuscle Muscle { get; set; }

        public ExerciseType DefaultType { get; set; }

        public ExerciseVariation Variation { get; set; }

        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
