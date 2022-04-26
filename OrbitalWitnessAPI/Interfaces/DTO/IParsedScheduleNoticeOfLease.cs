namespace OrbitalWitnessAPI.Interfaces
{
    public interface IParsedScheduleNoticeOfLease
    {
        string DateOfLeaseAndTerm { get; set; }
        string? EntryDate { get; set; }
        int EntryNumber { get; set; }
        string LesseesTitle { get; set; }
        List<string> Notes { get; set; }
        string PropertyDescription { get; set; }
        string RegistrationDateAndPlanRef { get; set; }
    }
}
