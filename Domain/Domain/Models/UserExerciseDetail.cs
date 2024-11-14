namespace Domain.Models
{
    public class UserExerciseDetail
    {
        public int UserDetailId { get; set; }
        public int UserExerciseId { get; set; }
        public int Series { get; set; }
        public string? Repetitions { get; set; }
        public string? RestTime { get; set; }
    }
}
