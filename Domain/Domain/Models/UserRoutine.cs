namespace Domain.Models
{
    public class UserRoutine
    {
        public int UserRoutineId { get; set; }
        public string? UserId { get; set; }
        public int RoutineTemplateId { get; set; }

        public List<UserExercise>? UserExercises { get; set; } = new List<UserExercise>();
    }
}
