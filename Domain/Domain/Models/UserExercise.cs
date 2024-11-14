namespace Domain.Models
{
    public class UserExercise
    {
        public int UserExerciseId { get; set; }
        public int UserRoutineId { get; set; }
        public int ExerciseTemplateId { get; set; }

        public List<UserExerciseDetail>? UserExerciseDetails { get; set; } = new List<UserExerciseDetail>();
    }
}
