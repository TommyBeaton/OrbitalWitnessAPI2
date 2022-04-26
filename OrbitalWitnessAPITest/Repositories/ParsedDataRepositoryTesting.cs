using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Context;
using OrbitalWitnessAPI.Domain;
using Microsoft.EntityFrameworkCore;
using OrbitalWitnessAPI.Repositories;

namespace OrbitalWitnessAPITest.Repositories
{
    public class ParsedDataRepositoryTesting
    {

        readonly ParsedSchedule _expectedParsedSchedule = new ParsedSchedule
        {
            RawData = "Raw Data",
            EntryNumber = 1,
            EntryDate = "01.01.1970",
            RegistrationDateAndPlanRef = "RegData",
            PropertyDescription = "PropDesc",
            DateOfLeaseAndTerm = "DateOfLease",
            LesseesTitle = "Title",
            Notes = new List<Note>
                {
                    new Note{Content = "NOTE A"},
                    new Note{Content = "NOTE B"}
                }
        };

        [Fact]
        public void SuccessfulCreate()
        {
            //Arrange
            var dbSetMock = new Mock<DbSet<ParsedSchedule>>();
            var contextMoq = new Mock<IOrbitalWitnessContext>();
            contextMoq.SetupGet(x => x.ParsedSchedules).Returns(dbSetMock.Object);
            var factory = new Mock<IParsedScheduleOrmFactory>();

            var repo = new ParsedDataRepository(contextMoq.Object, factory.Object);
            //act
            repo.Create(_expectedParsedSchedule);

            //assert
            contextMoq.Verify(x => x.ParsedSchedules.Add(
                It.Is<ParsedSchedule>(e => _expectedParsedSchedule.Equals(e)
                )), Times.Once());
            contextMoq.Verify(x => x.SaveChanges(), Times.Once());
        }
    
        [Fact]
        public void SuccessfulDelete()
        {
            //Arrange
            var dbSetMock = new Mock<DbSet<ParsedSchedule>>();
            var contextMoq = new Mock<IOrbitalWitnessContext>();
            contextMoq.SetupGet(x => x.ParsedSchedules).Returns(dbSetMock.Object);
            var factory = new Mock<IParsedScheduleOrmFactory>();

            var repo = new ParsedDataRepository(contextMoq.Object, factory.Object);
            //act
            repo.Delete(_expectedParsedSchedule);

            //assert
            contextMoq.Verify(x => x.ParsedSchedules.Remove(
                It.Is<ParsedSchedule>(e => _expectedParsedSchedule.Equals(e)
                )), Times.Once());
            contextMoq.Verify(x => x.SaveChanges(), Times.Once());
        }

    }
}
