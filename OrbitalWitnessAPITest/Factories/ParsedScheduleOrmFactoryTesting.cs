using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrbitalWitnessAPI.Factories;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.DTO;
using Xunit;
using Moq;

namespace OrbitalWitnessAPITest.Factories
{
    public class ParsedScheduleOrmFactoryTesting
    {
        [Theory]
        [InlineData("01.01.1970")]
        [InlineData(null)]
        public void DataIsCorrect(string? entryData)
        {
            //arrnage
            ParsedSchedule actualOutput;

            string entry1 = "Here                is";
            string entry2 = "A                   Way            To";
            string entry3 = "Format              Data            Data";

            List<string> rawContent = new List<string>
            {
                entry1,
                entry2,
                entry3
            };

            string expectedRawData = entry1 + entry2 + entry3;

            int expectedEntryNumber = 101;
            string expectedEntryDate = entryData;
            string expectedRegistration = "expectedReg";
            string expectedProp = "propDesc";
            string expectedDateOfLease = "dateOfLease";
            string expectedLesseesTitle = "lesseesTitle";
            List<string> expectedNotes = new List<string>
            {
                "NOTE a",
                "NOTE b",
                "NOTE c",
            };
            List<Note> notesList = new List<Note>();
            foreach (string note in expectedNotes) notesList.Add(new Note { Content = note });

            IParsedScheduleNoticeOfLease input = new ParsedScheduleNoticeOfLease
            {
                EntryNumber = expectedEntryNumber,
                EntryDate = expectedEntryDate,
                RegistrationDateAndPlanRef = expectedRegistration,
                PropertyDescription = expectedProp,
                DateOfLeaseAndTerm = expectedDateOfLease,
                LesseesTitle = expectedLesseesTitle,
                Notes = expectedNotes
            };

            ParsedSchedule expectedOutput = new ParsedSchedule
            {
                RawData = expectedRawData,
                EntryNumber = expectedEntryNumber,
                EntryDate = expectedEntryDate,
                RegistrationDateAndPlanRef = expectedRegistration,
                PropertyDescription = expectedProp,
                DateOfLeaseAndTerm = expectedDateOfLease,
                LesseesTitle = expectedLesseesTitle,
                Notes = notesList
            };

            var factory = new ParsedScheduleOrmFactory();

            //act
            actualOutput = factory.Create(rawContent, input);

            //assert
            Assert.Equal(expectedOutput, actualOutput);
        }
    }
}
