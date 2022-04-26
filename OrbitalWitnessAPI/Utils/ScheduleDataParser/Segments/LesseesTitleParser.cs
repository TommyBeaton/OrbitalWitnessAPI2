using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class LesseesTitleParser : IScheduleSegmentParser
    {
        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            if (rawInput.FormattedEntryText == null || rawInput.FormattedEntryText.Count <= 0) return;

            int lastIndex = rawInput.FormattedEntryText[0].Count - 1;

            if (lastIndex < 0) return;

            parsedOutput.LesseesTitle = rawInput.FormattedEntryText[0][lastIndex].Trim();
            rawInput.FormattedEntryText[0].RemoveAt(lastIndex);
        }
    }
}
