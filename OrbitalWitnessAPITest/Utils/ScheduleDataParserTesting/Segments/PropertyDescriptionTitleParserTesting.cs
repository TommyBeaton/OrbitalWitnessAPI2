using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using OrbitalWitnessAPI.Interfaces;
using OrbitalWitnessAPI.Utils.ScheduleDataParser;
using OrbitalWitnessAPITest.Extensions;

namespace OrbitalWitnessAPITest.Utils.ScheduleDataParserTesting.Segments
{
    public class PropertyDescriptionTitleParserTesting
    {

        //At this point, the algorithm would have removed the lessees title and the notes; so they don't need to be included in the test data
        public static IEnumerable<object[]> PropertyDescriptionTitleParserTestData()
        {
            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "09.07.2009",
                        "Endeavour House, 47 Cuba",
                        "06.07.2009"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "Street, London",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 2 in",
                        "1.1.2009"
                    },
                    new List<string>
                    {
                        "blue (part of)"
                    }
                },
                //Expected outcome
                "Endeavour House, 47 Cuba Street, London"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "15.11.2018",
                        "Ground Floor Premises",
                        "10.10.2018"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "from 10"
                    },
                    new List<string>
                    {
                        "numbered 2 in",
                        "October 2018"
                    },
                    new List<string>
                    {
                        "blue (part of)",
                        "to and"
                    },
                    new List<string>
                    {
                        "including 19"
                    },
                    new List<string>
                    {
                        "April 2028"
                    }
                },
                //Expected outcome
                "Ground Floor Premises"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "16.08.2013",
                        "21 Sheen Road (Ground floor",
                        "06.08.2013"
                    },
                    new List<string>
                    {
                        "shop)",
                        "Beginning on"
                    },
                    new List<string>
                    {
                        "and including"
                    },
                    new List<string>
                    {
                        "6.8.2013 and"
                    },
                    new List<string>
                    {
                        "ending on and"
                    },
                    new List<string>
                    {
                        "including"
                    },
                    new List<string>
                    {
                        "6.8.2023"
                    }
                },
                //Expected outcome
                "21 Sheen Road (Ground floor shop)"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "24.07.1989",
                        "17 Ashworth Close (Ground",
                        "01.06.1989"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "and First Floor Flat)",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 19",
                        "1.6.1989"
                    },
                    new List<string>
                    {
                        "(Part of) in"
                    },
                    new List<string>
                    {
                        "brown"
                    }
                },
                //Expected outcome
                "17 Ashworth Close (Ground and First Floor Flat)"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "19.09.1989",
                        "12 Harbord Close (Ground",
                        "01.09.1989"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "and First Floor Flat)",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 25",
                        "1.9.1989"
                    },
                    new List<string>
                    {
                        "(Part of) in"
                    },
                    new List<string>
                    {
                        "brown"
                    }
                },
                //Expected outcome
                "12 Harbord Close (Ground and First Floor Flat)"
            };
        }

        public static IEnumerable<object[]> CorrectDatadDeletedTestData()
        {
            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "09.07.2009",
                        "Endeavour House, 47 Cuba",
                        "06.07.2009"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "Street, London",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 2 in",
                        "1.1.2009"
                    },
                    new List<string>
                    {
                        "blue (part of)"
                    }
                },
                //Expected outcome
                 new List<List<string>> {
                    new List<string>
                    {
                        "09.07.2009",
                        "06.07.2009"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 2 in",
                        "1.1.2009"
                    },
                    new List<string>
                    {
                        "blue (part of)"
                    }
                }
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "15.11.2018",
                        "Ground Floor Premises",
                        "10.10.2018"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "from 10"
                    },
                    new List<string>
                    {
                        "numbered 2 in",
                        "October 2018"
                    },
                    new List<string>
                    {
                        "blue (part of)",
                        "to and"
                    },
                    new List<string>
                    {
                        "including 19"
                    },
                    new List<string>
                    {
                        "April 2028"
                    }
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "15.11.2018",
                        "10.10.2018"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "from 10"
                    },
                    new List<string>
                    {
                        "numbered 2 in",
                        "October 2018"
                    },
                    new List<string>
                    {
                        "blue (part of)",
                        "to and"
                    },
                    new List<string>
                    {
                        "including 19"
                    },
                    new List<string>
                    {
                        "April 2028"
                    }
                }
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "16.08.2013",
                        "21 Sheen Road (Ground floor",
                        "06.08.2013"
                    },
                    new List<string>
                    {
                        "shop)",
                        "Beginning on"
                    },
                    new List<string>
                    {
                        "and including"
                    },
                    new List<string>
                    {
                        "6.8.2013 and"
                    },
                    new List<string>
                    {
                        "ending on and"
                    },
                    new List<string>
                    {
                        "including"
                    },
                    new List<string>
                    {
                        "6.8.2023"
                    }
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "16.08.2013",
                        "06.08.2013"
                    },
                    new List<string>
                    {
                        "Beginning on"
                    },
                    new List<string>
                    {
                        "and including"
                    },
                    new List<string>
                    {
                        "6.8.2013 and"
                    },
                    new List<string>
                    {
                        "ending on and"
                    },
                    new List<string>
                    {
                        "including"
                    },
                    new List<string>
                    {
                        "6.8.2023"
                    }
                }
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "24.07.1989",
                        "17 Ashworth Close (Ground",
                        "01.06.1989"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "and First Floor Flat)",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 19",
                        "1.6.1989"
                    },
                    new List<string>
                    {
                        "(Part of) in"
                    },
                    new List<string>
                    {
                        "brown"
                    }
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "24.07.1989",
                        "01.06.1989"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 19",
                        "1.6.1989"
                    },
                    new List<string>
                    {
                        "(Part of) in"
                    },
                    new List<string>
                    {
                        "brown"
                    }
                }
            };

            yield return new object[]
            {
                //Input once processed into 2D array
                new List<List<string>> {
                    new List<string>
                    {
                        "19.09.1989",
                        "12 Harbord Close (Ground",
                        "01.09.1989"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "and First Floor Flat)",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 25",
                        "1.9.1989"
                    },
                    new List<string>
                    {
                        "(Part of) in"
                    },
                    new List<string>
                    {
                        "brown"
                    }
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "19.09.1989",
                        "01.09.1989"
                    },
                    new List<string>
                    {
                        "Edged and",
                        "125 years from"
                    },
                    new List<string>
                    {
                        "numbered 25",
                        "1.9.1989"
                    },
                    new List<string>
                    {
                        "(Part of) in"
                    },
                    new List<string>
                    {
                        "brown"
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(PropertyDescriptionTitleParserTestData))]
        public void ParsesCorrectData(List<List<string>> formattedData, string expectedOutcome)
        {
            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedData);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.PropertyDescription = expectedOutcome).Verifiable();
            var outputObj = outputMoq.Object;

            var parser = new PropertyDescriptionTitleParser();
            
            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify(); 
        }
    
        [Theory]
        [MemberData(nameof(CorrectDatadDeletedTestData))]
        public void DeletesCorrectData(List<List<string>> formattedData, List<List<string>> expectedOutput)
        {
            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedData);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            var outputObj = outputMoq.Object;

            var parser = new PropertyDescriptionTitleParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            Assert.True(expectedOutput.SequencesEqual(rawInputMoq.Object.FormattedEntryText));

        }
    }
}
