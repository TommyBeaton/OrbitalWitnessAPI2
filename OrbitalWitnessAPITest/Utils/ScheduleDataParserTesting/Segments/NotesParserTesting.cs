using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Utils.ScheduleDataParser;
using OrbitalWitnessAPITest.Extensions;

namespace OrbitalWitnessAPITest.Utils.ScheduleDataParserTesting.Segments
{
    public class NotesParserTesting
    {
        public static IEnumerable<object[]> AllNotesCapturedTestData()
        {
            yield return new object[]
            {
                new List<string>()
                {
                    "This",
                    "is",
                    "dummy",
                    "Data",
                    "NOTES: Note A",
                    "NOTES: Note B",
                    "Fake Note NOTES:",
                    "NOTES: Note C",
                    "Fin."
                },
                new List<string>()
                {
                    "NOTES: Note A",
                    "NOTES: Note B",
                    "NOTES: Note C"
                }
            };
        }

        public static IEnumerable<object[]> AllNotesRemovedTestData()
        {
            yield return new object[]
            {
                new List<string>()
                {
                    "This",
                    "is",
                    "dummy",
                    "Data",
                    "NOTES: Note A",
                    "NOTES: Note B",
                    "Fake Note NOTES:",
                    "NOTES: Note C",
                    "Fin."
                },
                new List<string>()
                {
                    "This",
                    "is",
                    "dummy",
                    "Data",
                    "Fake Note NOTES:",
                    "Fin."
                }
            };
        }

        [Theory]
        [MemberData(nameof(AllNotesCapturedTestData))]
        public void AllNotesCaptured(List<string> inputList, List<string> expectedOutput)
        {
            //Formatting here to save formatting from the test data function. Just a little neater
            List<List<string>> formattedEntry = new();
            foreach(string input in inputList)
            {
                formattedEntry.Add(new List<string>() { input });
            }

            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedEntry);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.Notes = expectedOutput).Verifiable();
            var outputObj = outputMoq.Object;

            var notesParser = new NotesParser();

            //act
            notesParser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify();
        }
    
        [Theory]
        [MemberData(nameof(AllNotesRemovedTestData))]
        public void AllNotesRemoved(List<string> inputList, List<string> expectedOutput)
        {
            //Formatting here to save formatting from the test data function. Just a little neater
            List<List<string>> formattedEntry = new();
            foreach (string input in inputList)
            {
                formattedEntry.Add(new List<string>() { input });
            }

            List<List<string>> formattedExpectedOutput = new();
            foreach (string output in expectedOutput)
            {
                formattedExpectedOutput.Add(new List<string>() { output });
            }

            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedEntry);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            var outputObj = outputMoq.Object;

            var notesParser = new NotesParser();

            //act
            notesParser.Parse(ref rawObj, ref outputObj);

            //assert
            Assert.True(formattedExpectedOutput.SequencesEqual(rawInputMoq.Object.FormattedEntryText));
        }
    }
}
