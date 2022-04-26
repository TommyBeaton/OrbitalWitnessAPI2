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
    public class EntryDateParserTesting
    {
        [Theory]
        [InlineData("01.01.1970")]
        [InlineData(null)]
        public void ParseWithValidData(string? expectedValue)
        {
            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.EntryDate).Returns(expectedValue);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.EntryDate = expectedValue).Verifiable();
            var outputObj = outputMoq.Object;

            var parser = new EntryDateParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify();
        }
    }
}
