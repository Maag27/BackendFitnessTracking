namespace Domain.Models
{
    public class Routine
    {
        public int RoutineTemplateId { get; set; }
        public string? RoutineName { get; set; }
        public string? UserId { get; set; }  // Agregamos UserId para asociar la rutina a un usuario

        public List<Exercise>? Exercises { get; set; } = new List<Exercise>();
    }
}
