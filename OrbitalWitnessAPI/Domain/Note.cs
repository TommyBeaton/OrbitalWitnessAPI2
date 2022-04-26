using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrbitalWitnessAPI.Domain
{
    public class Note : IEquatable<Note?>
    {
        [Key]
        public int NoteId { get; set; }
        public string Content { get; set; }

        public ParsedSchedule ParsedData { get; set; }
        public int ParsedDataId { get; set; }

        public override bool Equals(object? obj)
        {
            return Equals(obj as Note);
        }

        public bool Equals(Note? other)
        {
            return other != null &&
                   NoteId == other.NoteId &&
                   Content == other.Content &&
                   EqualityComparer<ParsedSchedule>.Default.Equals(ParsedData, other.ParsedData) &&
                   ParsedDataId == other.ParsedDataId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NoteId, Content, ParsedData, ParsedDataId);
        }
    }
}
