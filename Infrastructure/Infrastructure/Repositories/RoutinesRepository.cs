using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class RoutinesRepository
    {
        private readonly AppDbContext _context;

        public RoutinesRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Routine> AddRoutineAsync(Routine routine)
        {
            // Agregamos el objeto rutina
            _context.RoutineTemplates.Add(routine);
            await _context.SaveChangesAsync();
            return routine;
        }

        public async Task<List<Routine>> GetRoutinesByUserIdAsync(string userId)
        {
            // Comprobamos si RoutineTemplates no es null y si Exercises y UserId no son null
            return await _context.RoutineTemplates!
                .Where(r => r.UserId == userId && r.Exercises != null)
                .Include(r => r.Exercises!)
                .ThenInclude(e => e.ExerciseDetails!)
                .ToListAsync();
        }
    }
}
