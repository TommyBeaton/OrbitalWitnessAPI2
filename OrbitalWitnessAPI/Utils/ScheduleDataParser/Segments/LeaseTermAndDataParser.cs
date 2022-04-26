using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class LeaseTermAndDataParser : IScheduleSegmentParser
    {
        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            string output = string.Empty;

            foreach(var row in rawInput.FormattedEntryText)
            {
                foreach(var column in row)
                {
                    output += $" {column.Trim()}";
                }
            }
            
            parsedOutput.DateOfLeaseAndTerm = output.Trim();
        }
    }
}
