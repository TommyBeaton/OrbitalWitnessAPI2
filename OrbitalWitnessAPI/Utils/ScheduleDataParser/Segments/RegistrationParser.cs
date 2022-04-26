using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class RegistrationParser : IScheduleSegmentParser
    {
        private string FormatDataForEntry(string data) { return $" {data}"; }

        private Tuple<string, List<int>> ProcessComplexEntry(List<List<string>> data)
        {
            string output = string.Empty;
            List<int> indexes = new();

            //Start at 1 because 0 would have been processed before this
            for (int i = 1; i < data.Count; i++)
            {
                //Store in a nicer name + no need to keep on referencing the index
                string currentEntry = data[i][0];

                //Save where we are so it can be deleted later as to not shift any indexes
                indexes.Add(i);

                //Add current data
                output += FormatDataForEntry(currentEntry);

                //The string "(part of)" is contained in each instance where "Edged and" exists.
                //We can use this as a key to recognise the end.
                //The position of the closing bracket also determines the ending
                if (currentEntry.Contains("(part of)"))
                {
                    if (!currentEntry.EndsWith(")"))
                    {
                        //Accesses the instance where "brown" is the stragler.
                        output += FormatDataForEntry(data[i + 1][0]);
                        indexes.Add(i + 1);
                    }
                    
                    break;
                }
            }

            return new Tuple<string, List<int>>(output, indexes);
        }

        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            List<int> indexes = new();

            string registration = rawInput.FormattedEntryText[0][0];

            rawInput.FormattedEntryText[0].RemoveAt(0);

            if(rawInput.FormattedEntryText[1][0] == "Edged and")
            {
                var result = ProcessComplexEntry(rawInput.FormattedEntryText);
                registration += result.Item1;
                indexes.AddRange(result.Item2);    
            }

            parsedOutput.RegistrationDateAndPlanRef = registration;

            indexes = indexes.OrderByDescending(i => i).ToList();

            foreach (int i in indexes)
            {
                //Loop through and remove all instances of this entry from the first column
                rawInput.FormattedEntryText[i].RemoveAt(0);
                if(rawInput.FormattedEntryText[i].Count == 0) rawInput.FormattedEntryText.RemoveAt(i);
            }
                
        }
    }
}
