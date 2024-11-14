namespace Domain.Models
{
    public class ExerciseDetail
    {
        public int ExerciseDetailId { get; set; } // Nueva clave primaria

        public int DetailTemplateId { get; set; }
        public int ExerciseTemplateId { get; set; }
        public int Series { get; set; }
        public string? Repetitions { get; set; }
        public string? RestTime { get; set; }
    }
}
