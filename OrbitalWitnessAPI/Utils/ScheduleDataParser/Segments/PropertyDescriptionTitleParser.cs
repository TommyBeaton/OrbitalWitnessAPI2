using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class PropertyDescriptionTitleParser : IScheduleSegmentParser
    {
        private readonly string _key = "Edged and";

        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            string description = "";

            //The first input is always in this position
            description += rawInput.FormattedEntryText[0][1];
            rawInput.FormattedEntryText[0].RemoveAt(1);

            //Now check if the registration date exists on the second line
            //If not then capture the spill over for the description
            if (rawInput.FormattedEntryText[1][0] != _key) 
            {
                description += $" {rawInput.FormattedEntryText[1][0]}";
                rawInput.FormattedEntryText[1].RemoveAt(0);
            }
            //If the registration is on the second line 
            //And the lease term hasn't split into column B
            else if(rawInput.FormattedEntryText[1][0] == _key && rawInput.FormattedEntryText[1].ElementAtOrDefault(2) != null)
            {
                description += $" {rawInput.FormattedEntryText[1][1]}";
                rawInput.FormattedEntryText[1].RemoveAt(1);
            }
            

            parsedOutput.PropertyDescription = description;
        }
    }
}
