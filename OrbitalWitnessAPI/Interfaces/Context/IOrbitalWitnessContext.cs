using Microsoft.EntityFrameworkCore;
using OrbitalWitnessAPI.Domain;

namespace OrbitalWitnessAPI.Interfaces
{
    public interface IOrbitalWitnessContext
    {
        DbSet<Note> Notes { get; set; }
        DbSet<ParsedSchedule> ParsedSchedules { get; set; }
        int SaveChanges();
    }
}