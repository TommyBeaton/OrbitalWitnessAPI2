using OrbitalWitnessAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrbitalWitnessAPI.Utils.ScheduleDataParser
{
    public class NotesParser : IScheduleSegmentParser
    {
        public void Parse(ref IRawScheduleNoticeOfLease rawInput, ref IParsedScheduleNoticeOfLease parsedOutput)
        {
            //prep lists
            List<int> noteIndexes = new();
            List<string> notes = new();

            string currentEntry;

            //loop through each of the formatted entries
            for(int i = 0; i< rawInput.FormattedEntryText.Count; i++)
            {
                //the index of a note is always 0
                currentEntry = rawInput.FormattedEntryText[i][0];

                //if the current entry is a note
                if (currentEntry.StartsWith("NOTE"))
                {
                    //Loop through this part of the list
                    notes.Add(currentEntry);
                                        
                    //store this index to have it removed later
                    noteIndexes.Add(i);
                }
            }

            //reverse the list to not unalign indexes
            noteIndexes = noteIndexes.OrderByDescending(i => i).ToList();

            //remove indexes
            foreach(int i in noteIndexes)
                rawInput.FormattedEntryText.RemoveAt(i);

            //Add the notes
            parsedOutput.Notes = notes;
        }
    }
}
