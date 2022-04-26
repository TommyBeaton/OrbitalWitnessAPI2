using OrbitalWitnessAPI.Domain;

namespace OrbitalWitnessAPI.Interfaces
{
    public interface IParsedDataRepository : IRepository<ParsedSchedule, int>
    {
        ParsedSchedule FindByContent(string content);
    }
}
