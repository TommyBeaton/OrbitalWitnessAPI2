using OrbitalWitnessAPI.Domain;

namespace OrbitalWitnessAPI.Interfaces
{
    /// <summary>
    /// Factory to parse a domain object to a DTO
    /// </summary>
    public interface IParsedScheduleDtoFactory
    {
        /// <summary>
        /// Convert a domain object to a DTO
        /// </summary>
        /// <param name="data">The object to base the DTO off of</param>
        /// <returns>A parsed DTO</returns>
        public IParsedScheduleNoticeOfLease Create(ParsedSchedule data);
    }
}
