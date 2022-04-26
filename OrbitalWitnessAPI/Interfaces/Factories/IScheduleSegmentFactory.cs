using OrbitalWitnessAPI.Utils;

namespace OrbitalWitnessAPI.Interfaces
{
    /// <summary>
    /// A factory to create the different segment parsers for the schedule parser
    /// </summary>
    public interface IScheduleSegmentFactory
    {
        /// <summary>
        /// Create
        /// </summary>
        /// <param name="segment">The segment to be returned</param>
        /// <returns>A segment parser</returns>
        IScheduleSegmentParser CreateScheduleSegmentParser(SegmentParserSegments segment);
    }
}
