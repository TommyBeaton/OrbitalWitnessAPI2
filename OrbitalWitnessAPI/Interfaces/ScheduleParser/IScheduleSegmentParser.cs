namespace OrbitalWitnessAPI.Interfaces
{
    public interface IScheduleSegmentParser
    {
        //Parse a segment of the raw data
        void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput);
    }
}
