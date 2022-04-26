using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class EntryDateParser : IScheduleSegmentParser
    {
        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            parsedOutput.EntryDate = string.IsNullOrEmpty(rawInput.EntryDate) ? null : rawInput.EntryDate;
        }
    }
}
