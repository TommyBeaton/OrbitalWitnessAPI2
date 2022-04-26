namespace OrbitalWitnessAPI.Interfaces
{
    public interface IRawScheduleNoticeOfLease
    {
        string? EntryDate { get; set; }
        string EntryNumber { get; set; }
        List<string> EntryText { get; set; }
        string EntryType { get; set; }
        List<List<string>> FormattedEntryText { get; set; }
    }
}
