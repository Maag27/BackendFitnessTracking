namespace Domain.Models
{
    public class Exercise
    {
        public int ExerciseId { get; set; } // Nueva clave primaria

        public int ExerciseTemplateId { get; set; }
        public int RoutineTemplateId { get; set; }
        public string? ExerciseName { get; set; }

        public List<ExerciseDetail>? ExerciseDetails { get; set; } = new List<ExerciseDetail>();
    }
}
