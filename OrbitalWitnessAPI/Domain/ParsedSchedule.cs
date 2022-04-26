using OrbitalWitnessAPI.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitalWitnessAPI.Domain
{
    [Table("ParsedSchedules")]
    public class ParsedSchedule : IEquatable<ParsedSchedule?>
    {
        [Key]
        public int ParsedDataId { get; set; }
        public string RawData { get; set; }
        public int EntryNumber { get; set; }
        public string? EntryDate { get; set; }
        public string RegistrationDateAndPlanRef { get; set; }
        public string PropertyDescription { get; set; }
        public string DateOfLeaseAndTerm { get; set; }
        public string LesseesTitle { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();

        public override bool Equals(object? obj)
        {
            return Equals(obj as ParsedSchedule);
        }

        public bool Equals(ParsedSchedule? other)
        {
            return other != null &&
                   ParsedDataId == other.ParsedDataId &&
                   RawData == other.RawData &&
                   EntryNumber == other.EntryNumber &&
                   EntryDate == other.EntryDate &&
                   RegistrationDateAndPlanRef == other.RegistrationDateAndPlanRef &&
                   PropertyDescription == other.PropertyDescription &&
                   DateOfLeaseAndTerm == other.DateOfLeaseAndTerm &&
                   LesseesTitle == other.LesseesTitle &&
                   Notes.SequenceEqual(other.Notes);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(ParsedDataId);
            hash.Add(RawData);
            hash.Add(EntryNumber);
            hash.Add(EntryDate);
            hash.Add(RegistrationDateAndPlanRef);
            hash.Add(PropertyDescription);
            hash.Add(DateOfLeaseAndTerm);
            hash.Add(LesseesTitle);
            hash.Add(Notes);
            return hash.ToHashCode();
        }
    }
}
