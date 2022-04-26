using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrbitalWitnessAPI.Factories;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Utils;
using OrbitalWitnessAPI.Utils.ScheduleDataParser;
using Xunit;

namespace OrbitalWitnessAPITest.Factories
{
    public class ScheduleSegmentFactoryTesting
    {
        [Theory]
        [InlineData(SegmentParserSegments.EntryNumber, typeof(EntryNumberParser))]
        [InlineData(SegmentParserSegments.EntryDate, typeof(EntryDateParser))]
        [InlineData(SegmentParserSegments.LesseesTitle, typeof(LesseesTitleParser))]
        [InlineData(SegmentParserSegments.Notes, typeof(NotesParser))]
        [InlineData(SegmentParserSegments.PropDescription, typeof(PropertyDescriptionTitleParser))]
        [InlineData(SegmentParserSegments.Registration, typeof(RegistrationParser))]
        [InlineData(SegmentParserSegments.LeaseTermDate, typeof(LeaseTermAndDataParser))]
        public void CorrectTypeReturned(SegmentParserSegments requestedType, Type type)
        {
            //arrange
            IScheduleSegmentParser actualResult;
            var factory = new ScheduleSegmentFactory();

            //act
            actualResult = factory.CreateScheduleSegmentParser(requestedType);

            //assert
            Assert.Equal(type, actualResult.GetType());
        }
    }
}
