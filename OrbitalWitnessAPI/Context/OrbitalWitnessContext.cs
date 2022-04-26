using Microsoft.EntityFrameworkCore;
using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.Context
{
    public class OrbitalWitnessContext : DbContext, IOrbitalWitnessContext
    {
        public OrbitalWitnessContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ParsedSchedule> ParsedSchedules { get; set; }
        public DbSet<Note> Notes { get; set; }
    }
}
