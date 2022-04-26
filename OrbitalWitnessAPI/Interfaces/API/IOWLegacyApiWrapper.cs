using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.Interfaces
{
    /// <summary>
    /// Wrapper for the legacy api
    /// </summary>
    public interface IOWLegacyApiWrapper
    {
        /// <summary>
        /// Get the raw schedule data from the OW API
        /// </summary>
        /// <returns>A list of raw data</returns>
        Task<IList<RawScheduleNoticeOfLease>> GetSchedules();
    }
}