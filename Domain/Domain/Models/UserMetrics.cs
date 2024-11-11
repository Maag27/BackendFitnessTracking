namespace Domain.Models
{
    public class UserMetrics
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        
        public string? Nombre { get; set; } 
        public string? Apellido { get; set; } 

        public int Edad { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
    }
}
