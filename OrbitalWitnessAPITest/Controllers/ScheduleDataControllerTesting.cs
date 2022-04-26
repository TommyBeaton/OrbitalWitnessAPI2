using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OrbitalWitnessAPI.Controllers;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using OrbitalWitnessAPI.Domain;

namespace OrbitalWitnessAPITest.Controllers
{
    public class ScheduleDataControllerTesting
    {
        readonly List<RawScheduleNoticeOfLease> _standardRawResult = new List<RawScheduleNoticeOfLease>
        {
            new RawScheduleNoticeOfLease
            {
                EntryNumber = "1",
                EntryDate = "01.01.1970",
                EntryType = "Type",
                EntryText = new List<string>
                {
                    "Value1",
                    "Value2",
                    "Value3"
                }
            }
        };

        readonly IParsedScheduleNoticeOfLease _expectedResponse = new ParsedScheduleNoticeOfLease
        {
            EntryNumber = 5,
            EntryDate = "EntryDate",
            RegistrationDateAndPlanRef = "Reg",
            PropertyDescription = "Prop",
            DateOfLeaseAndTerm = "DateOfLease",
            LesseesTitle = "Lessee",
            Notes = new List<string>
            {
                "Value1",
                "Value2",
                "Value3"
            }
        };

        readonly ParsedSchedule _domainData = new ParsedSchedule
        {
            EntryNumber = 5,
            EntryDate = "EntryDate",
            RegistrationDateAndPlanRef = "Reg",
            PropertyDescription = "Prop",
            DateOfLeaseAndTerm = "DateOfLease",
            LesseesTitle = "Lessee",
            Notes = new List<Note>
            {
                new Note{Content = "Value1"},
                new Note{Content = "Value2"},
                new Note{Content = "Value3"}
            }
        };

        //Api is called
        [Fact]
        public void ApiIsCalled()
        {
            //arrange
            var apiWrapperMock = new Mock<IOWLegacyApiWrapper>();
            apiWrapperMock.Setup(x => x.GetSchedules())
                .Returns(Task.FromResult<IList<RawScheduleNoticeOfLease>>(new List<RawScheduleNoticeOfLease>()));
            
            var reposioryMock = new Mock<IParsedDataRepository>();
            var parserMock = new Mock<IScheduleParser>();

            var controller = new ScheduleDataController(reposioryMock.Object, null, apiWrapperMock.Object, parserMock.Object, null);

            //act
            controller.Get();

            //assert
            apiWrapperMock.Verify(
                x => x.GetSchedules(),
                Times.Once());

            reposioryMock.Verify(
                x => x.FindByContent(
                    It.IsAny<string>()), 
                Times.Never());

            parserMock.Verify(
                x => x.Parse(
                    It.IsAny<IRawScheduleNoticeOfLease>()), 
                Times.Never());
        }

        //Api returns nothing
        [Fact]
        public void ApiCantReturnlegacyData()
        {
            //arrange
            string expectedException = "No Auth on API";
            int expectedStatusCode = 204;
            ObjectResult result;
            var apiWrapperMock = new Mock<IOWLegacyApiWrapper>();
            apiWrapperMock.Setup(
                x => x.GetSchedules())
                .Throws(new UnauthorizedAccessException(expectedException));

            var reposioryMock = new Mock<IParsedDataRepository>();
            var parserMock = new Mock<IScheduleParser>();

            var controller = new ScheduleDataController(reposioryMock.Object, null, apiWrapperMock.Object, parserMock.Object, null);

            //act
            result = (ObjectResult)controller.Get().Result.Result;

            //assert
            Assert.Equal(expectedException, result.Value);
            Assert.Equal(expectedStatusCode, result.StatusCode);

            apiWrapperMock.Verify(
                x => x.GetSchedules(), 
                Times.Once());

            reposioryMock.Verify(
                x => x.FindByContent(
                    It.IsAny<string>()),
                Times.Never());

            parserMock.Verify(
                x => x.Parse(
                    It.IsAny<IRawScheduleNoticeOfLease>()),
                Times.Never());
        }

        //If db returns value, parser isnt called
        [Fact]
        public void ParserIgnoredIfDbHasValue()
        {
            //arrange
            int expectedStatusCode = 200;
            ObjectResult result;
            IList<IParsedScheduleNoticeOfLease> formattedExpectedResponse = new List<IParsedScheduleNoticeOfLease>
            {
                _expectedResponse
            };

            var apiWrapperMock = new Mock<IOWLegacyApiWrapper>();
            apiWrapperMock.Setup(
                x => x.GetSchedules())
                .Returns(Task.FromResult<IList<RawScheduleNoticeOfLease>>(_standardRawResult));

            var reposioryMock = new Mock<IParsedDataRepository>();
            reposioryMock.Setup(
                x => x.FindByContent(
                    It.IsAny<string>()))
                .Returns(_domainData);

            var dtoFactoryMock = new Mock<IParsedScheduleDtoFactory>();
            dtoFactoryMock.Setup(
                x => x.Create(
                    It.IsAny<ParsedSchedule>()))
                .Returns(_expectedResponse);

            var parserMock = new Mock<IScheduleParser>();

            var controller = new ScheduleDataController(reposioryMock.Object, dtoFactoryMock.Object, apiWrapperMock.Object, parserMock.Object, null);

            //act
            result = (ObjectResult)controller.Get().Result.Result;

            //assert
            Assert.Equal(formattedExpectedResponse, result.Value);
            Assert.Equal(expectedStatusCode, result.StatusCode);

            apiWrapperMock.Verify(
                x => x.GetSchedules(), 
                Times.Once());

            reposioryMock.Verify(
                x => x.FindByContent(
                    It.IsAny<string>()), 
                Times.Once());

            dtoFactoryMock.Verify(
                x => x.Create(
                    It.IsAny<ParsedSchedule>()),
                Times.Once());

            parserMock.Verify(
                x => x.Parse(
                    It.IsAny<IRawScheduleNoticeOfLease>()), 
                Times.Never());
        }

        //if db doesnt return value parser is called and db is updated
        [Fact]
        public void ParserCalledIfDbReturnsNoValue()
        {
            //arrange
            int expectedStatusCode = 200;
            ObjectResult result;
            IList<IParsedScheduleNoticeOfLease> formattedExpectedResponse = new List<IParsedScheduleNoticeOfLease>
            {
                _expectedResponse
            };

            var apiWrapperMock = new Mock<IOWLegacyApiWrapper>();
            apiWrapperMock.Setup(
                x => x.GetSchedules())
                .Returns(Task.FromResult<IList<RawScheduleNoticeOfLease>>(_standardRawResult));

            var reposioryMock = new Mock<IParsedDataRepository>();
            reposioryMock.Setup(
                x => x.FindByContent(
                    It.IsAny<string>()))
                .Returns<IEnumerable<string>>(null);

            var dtoFactoryMock = new Mock<IParsedScheduleDtoFactory>();
            
            var ormFactoryMock = new Mock<IParsedScheduleOrmFactory>();

            var parserMock = new Mock<IScheduleParser>();
            parserMock.Setup(
                x => x.Parse(
                    It.IsAny<IRawScheduleNoticeOfLease>()))
                .Returns(_expectedResponse);

            var controller = new ScheduleDataController(reposioryMock.Object, dtoFactoryMock.Object, apiWrapperMock.Object, parserMock.Object, ormFactoryMock.Object);

            //act
            result = (ObjectResult)controller.Get().Result.Result;

            //assert
            Assert.Equal(formattedExpectedResponse, result.Value);
            Assert.Equal(expectedStatusCode, result.StatusCode);

            apiWrapperMock.Verify(
                x => x.GetSchedules(), 
                Times.Once());

            reposioryMock.Verify(
                x => x.FindByContent(
                    It.IsAny<string>()), 
                Times.Once());

            dtoFactoryMock.Verify(
                x => x.Create(
                    It.IsAny<ParsedSchedule>()), 
                Times.Never());

            parserMock.Verify(
                x => x.Parse(
                    It.IsAny<IRawScheduleNoticeOfLease>()), 
                Times.Once());

            reposioryMock.Verify(
                x => x.Create(
                    It.IsAny<ParsedSchedule>()), 
                Times.Once());
        }
    }
}
