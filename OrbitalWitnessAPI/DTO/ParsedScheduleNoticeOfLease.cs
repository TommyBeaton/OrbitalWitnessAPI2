using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.DTO
{
    public class ParsedScheduleNoticeOfLease : IParsedScheduleNoticeOfLease, IEquatable<ParsedScheduleNoticeOfLease?>
    {
        /// <summary>
        /// The Entry Number
        /// </summary>
        public int EntryNumber { get; set; }

        /// <summary>
        /// Date the Entry was added
        /// </summary>
        public string? EntryDate { get; set; }

        /// <summary>
        /// Date the Entry registers and where it referred to on the Title Plan
        /// </summary>    
        public string RegistrationDateAndPlanRef { get; set; } = string.Empty;

        /// <summary>
        /// A brief description of the property
        /// </summary>
        public string PropertyDescription { get; set; } = string.Empty;

        /// <summary>
        /// Date the Lease was created from and how long it will live for.
        /// </summary>
        public string DateOfLeaseAndTerm { get; set; } = string.Empty;

        /// <summary>
        /// Title number of the Lessee
        /// </summary>
        public string LesseesTitle { get; set; } = string.Empty;

        /// <summary>
        /// All appended Notes to the Entry.
        /// </summary>
        public List<string> Notes { get; set; } = new List<string>();

        public override bool Equals(object? obj)
        {
            return Equals(obj as ParsedScheduleNoticeOfLease);
        }

        public bool Equals(ParsedScheduleNoticeOfLease? other)
        {
            return other != null &&
                   EntryNumber == other.EntryNumber &&
                   EntryDate == other.EntryDate &&
                   RegistrationDateAndPlanRef == other.RegistrationDateAndPlanRef &&
                   PropertyDescription == other.PropertyDescription &&
                   DateOfLeaseAndTerm == other.DateOfLeaseAndTerm &&
                   LesseesTitle == other.LesseesTitle &&
                   EqualityComparer<List<string>>.Default.Equals(Notes, other.Notes);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(EntryNumber, EntryDate, RegistrationDateAndPlanRef, PropertyDescription, DateOfLeaseAndTerm, LesseesTitle, Notes);
        }
    }
}
