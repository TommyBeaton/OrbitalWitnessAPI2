using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Factories;

namespace OrbitalWitnessAPITest.Factories
{
    public class ParsedScheduleDtoFactoryTesting
    {
        [Theory]
        [InlineData("Date")]
        [InlineData(null)]
        public void ReturnsCorrectData(string? date)
        {
            //arrange
            IParsedScheduleNoticeOfLease actualOutput;

            int entryNumber = 101;
            string entryDate = date;
            string registrationDateAndPlanRef = "regAndPlanRef";
            string propertyDescription = "propDesc";
            string dateOfLeaseAndTerm = "dateOfLeaseAndTerm";
            string lesseesTitle = "lesseesTitle";
            List<string> notes = new List<string>()
            {
                "Note a",
                "Note b"
            };

            
            var expectedOutcome = new ParsedScheduleNoticeOfLease
            {
                EntryNumber = entryNumber,
                EntryDate = entryDate,
                RegistrationDateAndPlanRef = registrationDateAndPlanRef,
                PropertyDescription = propertyDescription,
                DateOfLeaseAndTerm = dateOfLeaseAndTerm,
                LesseesTitle = lesseesTitle,
                Notes = notes
            };

            var input = new ParsedSchedule
            {
                EntryNumber = entryNumber,
                EntryDate = entryDate,
                RegistrationDateAndPlanRef = registrationDateAndPlanRef,
                PropertyDescription = propertyDescription,
                DateOfLeaseAndTerm = dateOfLeaseAndTerm,
                LesseesTitle = lesseesTitle
            };
            foreach(var note in notes) input.Notes.Add(new Note { Content = note });

            var factory = new ParsedScheduleDtoFactory();

            //act
            actualOutput = factory.Create(input);

            //assert
            Assert.Equal(expectedOutcome, actualOutput);
        }
    }
}
