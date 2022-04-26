using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.DTO
{
    public class RawScheduleNoticeOfLease : IRawScheduleNoticeOfLease
    {
        /// <summary>
        /// Entry Number of Entry
        /// </summary>
        public string EntryNumber { get; set; } = string.Empty;

        /// <summary>
        /// Date the Entry was added
        /// </summary>
        public string? EntryDate { get; set; } = string.Empty;

        /// <summary>
        /// Type of the Entry
        /// </summary>
        public string EntryType { get; set; } = string.Empty;

        /// <summary>
        /// Details of the Entry
        /// </summary>
        public List<string> EntryText { get; set; } = new List<string>();
        public List<List<string>> FormattedEntryText { get; set; } = new List<List<string>>();
    }
}
