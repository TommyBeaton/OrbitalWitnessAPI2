namespace OrbitalWitnessAPI.Interfaces
{
    public interface IScheduleParser
    {
        /// <summary>
        /// Parse raw data
        /// </summary>
        /// <param name="data">Raw data to be parsed</param>
        /// <returns>The parsed data</returns>
        public IParsedScheduleNoticeOfLease Parse(IRawScheduleNoticeOfLease data);
    }
}
