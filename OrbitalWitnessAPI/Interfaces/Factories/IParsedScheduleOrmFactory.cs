using OrbitalWitnessAPI.Domain;

namespace OrbitalWitnessAPI.Interfaces
{
    /// <summary>
    /// Factory to parse a DTO ot a domain object
    /// </summary>
    public interface IParsedScheduleOrmFactory
    {
        /// <summary>
        /// Convert a DTO ot a domain object
        /// </summary>
        /// <param name="data">The object to base the domain object off of</param>
        /// <returns>A parsed domain object</returns>
        public ParsedSchedule Create(List<string> rawContent, IParsedScheduleNoticeOfLease data);
    }
}
