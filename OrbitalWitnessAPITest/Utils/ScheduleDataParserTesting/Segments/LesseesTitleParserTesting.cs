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
    public class LesseesTitleParserTesting
    {
        [Fact]
        public void GetLesseesTitle()
        {
            //arrange
            string expected = "foobar";

            List<List<string>> formattedList = new() { 
                new List<string>()
                { 
                    "some",
                    "fake",
                    "text",
                    expected
                }
            };

            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedList);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.LesseesTitle = expected).Verifiable();
            var outputObj = outputMoq.Object;

            var parser = new LesseesTitleParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify();
        }

        [Fact]
        public void TryParseEmptyList1()
        {
            //arrange
            List<List<string>> formattedList = new()
            {
                new List<string>()
            };

            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedList);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            var outputObj = outputMoq.Object;

            var parser = new LesseesTitleParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.VerifySet(x => x.LesseesTitle = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void TryParseEmptyList2()
        {
            //arrange
            List<List<string>> formattedList = new();

            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedList);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            var outputObj = outputMoq.Object;

            var parser = new LesseesTitleParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.VerifySet(x => x.LesseesTitle = It.IsAny<string>(), Times.Never());
        }

        [Fact]
        public void LesseesTitleIsRemoved()
        {
            //arrange
            List<List<string>> expectedList = new()
            {
                new List<string>()
                {
                    "some",
                    "fake",
                    "text"
                }
            };

            List<List<string>> formattedList = expectedList;
            formattedList[0].Add("itemToBeRemoved");

            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedList);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            var outputObj = outputMoq.Object;

            var parser = new LesseesTitleParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            Assert.True(expectedList.SequencesEqual(rawInputMoq.Object.FormattedEntryText));
        }
    }
}
