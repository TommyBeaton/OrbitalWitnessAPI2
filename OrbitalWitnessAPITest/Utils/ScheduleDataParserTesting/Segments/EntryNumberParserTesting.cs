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
using OrbitalWitnessAPI.Factories;


namespace OrbitalWitnessAPITest.Utils.ScheduleDataParserTesting.Segments
{
    public class EntryNumberParserTesting
    {
        [Fact]
        public void ParseWithValidData()
        {
            //arrange
            int expectedNumber = int.MaxValue;

            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.EntryNumber).Returns(expectedNumber.ToString());
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.EntryNumber = expectedNumber).Verifiable();
            var outputObj = outputMoq.Object;

            var parser = new EntryNumberParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify();
        }

        [Fact]
        public void ParseWithInvalidData()
        {
            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.EntryNumber).Returns(string.Empty);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupProperty(m => m.EntryNumber, int.MinValue);
            var outputObj = outputMoq.Object;

            var parser = new EntryNumberParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.VerifySet(x => x.EntryNumber = It.IsAny<int>(), Times.Never());
        }
    }
}
