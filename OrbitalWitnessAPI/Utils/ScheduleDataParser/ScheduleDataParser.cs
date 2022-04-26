using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Interfaces;
using System.Text.RegularExpressions;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class ScheduleDataParser : IScheduleParser
    {
        private readonly IScheduleSegmentFactory _factory;

        //Note: this will run in order. The order must be: Lessees title, Notes, prop description, reg date, date of lease and term
        private readonly IList<SegmentParserSegments> _segments = new List<SegmentParserSegments>()
        {
            SegmentParserSegments.EntryNumber,
            SegmentParserSegments.EntryDate,
            SegmentParserSegments.LesseesTitle,
            SegmentParserSegments.Notes,
            SegmentParserSegments.PropDescription,
            SegmentParserSegments.Registration,
            SegmentParserSegments.LeaseTermDate
        };

        public ScheduleDataParser(IScheduleSegmentFactory factory)
        {
            _factory = factory;
        }

        private List<List<string>> FormatEntryText(List<string> input)
        {
            var output = new List<List<string>>();

            foreach (var entry in input)
            {
                //Needed to cover any extra spaces that exist in lists
                if (entry.StartsWith("NOTE"))
                {
                    output.Add(new List<string>() { entry.Trim() });
                    continue;
                }
                string trimmedEntry = entry.Trim();
                output.Add(Regex.Split(trimmedEntry, @"\s{2,}").ToList());
            }

            return output;
        }

        public IParsedScheduleNoticeOfLease Parse(IRawScheduleNoticeOfLease data)
        {
            IParsedScheduleNoticeOfLease parsedData = new ParsedScheduleNoticeOfLease();

            data.FormattedEntryText = FormatEntryText(data.EntryText);

            foreach (var segment in _segments)
            {
                var segmentParser = _factory.CreateScheduleSegmentParser(segment);

                if (segmentParser != null)
                    segmentParser.Parse(ref data, ref parsedData);
            }

            return parsedData;
        }
    }
}
