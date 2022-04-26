using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Utils;
using OrbitalWitnessAPI.Utils.ScheduleDataParser;

namespace OrbitalWitnessAPI.Factories
{
    public class ScheduleSegmentFactory : IScheduleSegmentFactory
    {
        public IScheduleSegmentParser CreateScheduleSegmentParser(SegmentParserSegments segment)
        {
            return segment switch
            {
                SegmentParserSegments.EntryNumber => new EntryNumberParser(),
                SegmentParserSegments.EntryDate => new EntryDateParser(),
                SegmentParserSegments.LesseesTitle => new LesseesTitleParser(),
                SegmentParserSegments.Notes => new NotesParser(),
                SegmentParserSegments.PropDescription => new PropertyDescriptionTitleParser(),
                SegmentParserSegments.Registration => new RegistrationParser(),
                SegmentParserSegments.LeaseTermDate => new LeaseTermAndDataParser(),
                _ => null
            };
        }
    }
}
