using Domain.Enum;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
     //   public WorkoutDay WorkoutDay { get; set; }

     
        public int ExerciseTemplateId { get; set; }
       // public ExerciseTemplate ExerciseTemplate { get; set; }

     
        public int Sets { get; set; }

        public int MinReps { get; set; }
        public int MaxReps { get; set; }

        public string Rest { get; set; } = "";

        public string? Note { get; set; }

        [ValidateNever] //  stops "WorkoutDay field is required" error
        public WorkoutDay WorkoutDay { get; set; } = null!;

        [ValidateNever] // stops "ExerciseTemplate field is required" error
        public ExerciseTemplate ExerciseTemplate { get; set; } = null!;

    }
}
