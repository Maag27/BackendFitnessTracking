namespace Domain.Models
{
    public class UserMetrics
    {
        public int Id { get; set; }
        public string? UserId { get; set; } // ID del usuario desde Firebase
        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
