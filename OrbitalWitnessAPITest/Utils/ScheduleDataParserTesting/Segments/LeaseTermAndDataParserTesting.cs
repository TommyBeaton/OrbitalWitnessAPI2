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
    public class LeaseTermAndDataParserTesting
    {
        public static IEnumerable<object[]> Data()
        {
            yield return new object[] { 
                new List<List<string>>
                {
                    new List<string> { "Hello" },
                    new List<string> { "World" }
                },
                "Hello World"
            };

            yield return new object[] {
                new List<List<string>>
                {
                    new List<string> { "A", "B" },
                    new List<string> { "C" },
                    new List<string> { "D" }
                },
                "A B C D"
            };

            yield return new object[] {
                new List<List<string>>
                {
                    new List<string> { "A     ", "B   " },
                    new List<string> { "    C" },
                    new List<string> { "D    " }
                },
                "A B C D"
            };

            yield return new object[] {
                new List<List<string>>
                {
                    new List<string> { "A", "B" },
                    new List<string> { "C", "D" },
                    new List<string> { "E" },
                    new List<string> { "F", "G" }
                },
                "A B C D E F G"
            };
        }


        [Theory]
        [MemberData(nameof(Data))]
        public void AllDataCaptured(List<List<string>> data, string expected)
        {
            //arrange
            var rawInput = new Mock<IRawScheduleNoticeOfLease>();
            rawInput.SetupProperty(x => x.FormattedEntryText, data);
            var rawObj = rawInput.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.DateOfLeaseAndTerm = expected).Verifiable();
            var outputObj = outputMoq.Object;

            var parser = new LeaseTermAndDataParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify();
        }
    }
}
