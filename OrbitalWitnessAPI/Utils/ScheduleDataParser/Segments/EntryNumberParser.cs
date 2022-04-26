using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class EntryNumberParser : IScheduleSegmentParser
    {
        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            bool result = int.TryParse(rawInput.EntryNumber, out int entryNumber);

            if (result) parsedOutput.EntryNumber = entryNumber;
        }
    }
}
