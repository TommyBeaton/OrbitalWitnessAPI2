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
    public class RegistrationParserTesting
    {
        public static IEnumerable<object[]> RegistrationParserTestData()
        {
            yield return new object[]
            {
                //Input once processed into 2D array
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
                },
                //Expected outcome
                "09.07.2009 Edged and numbered 2 in blue (part of)"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
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
                },
                //Expected outcome
                "15.11.2018 Edged and numbered 2 in blue (part of)"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
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
                },
                //Expected outcome
                "16.08.2013"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
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
                },
                //Expected outcome
                //This has been edited from the given data.
                //This is because the raw data says "numbered 19" and the parsed is "numbered 25"
                "24.07.1989 Edged and numbered 19 (Part of) in brown"
            };

            yield return new object[]
            {
                //Input once processed into 2D array
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
                },
                //Expected outcome
                "19.09.1989 Edged and numbered 25 (Part of) in brown"
            };
        }

        public static IEnumerable<object[]> DeletesCorrectEntriesTestData()
        {
            yield return new object[]
            {
                //Input once processed into 2D array
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
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "06.07.2009"
                    },
                    new List<string>
                    {
                        "125 years from"
                    },
                    new List<string>
                    {
                        "1.1.2009"
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
                        "10.10.2018"
                    },
                    new List<string>
                    {
                        "from 10"
                    },
                    new List<string>
                    {
                        "October 2018"
                    },
                    new List<string>
                    {
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
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
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
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "01.06.1989"
                    },
                    new List<string>
                    {
                        "125 years from"
                    },
                    new List<string>
                    {
                        "1.6.1989"
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
                },
                //Expected outcome
                new List<List<string>> {
                    new List<string>
                    {
                        "01.09.1989"
                    },
                    new List<string>
                    {
                        "125 years from"
                    },
                    new List<string>
                    {
                        "1.9.1989"
                    }
                }
            };
        }

        [Theory]
        [MemberData(nameof(RegistrationParserTestData))]
        public void ParsesCorrectData(List<List<string>> formattedData, string expectedOutcome)
        {
            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedData);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            outputMoq.SetupSet(m => m.RegistrationDateAndPlanRef = expectedOutcome).Verifiable();
            var outputObj = outputMoq.Object;

            var parser = new RegistrationParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            outputMoq.Verify();
        }

        [Theory]
        [MemberData(nameof(DeletesCorrectEntriesTestData))]
        public void DeletesCorrectEntries(List<List<string>> formattedData, List<List<string>> expectedOutcome)
        {
            //arrange
            var rawInputMoq = new Mock<IRawScheduleNoticeOfLease>();
            rawInputMoq.SetupGet(x => x.FormattedEntryText).Returns(formattedData);
            var rawObj = rawInputMoq.Object;

            var outputMoq = new Mock<IParsedScheduleNoticeOfLease>();
            var outputObj = outputMoq.Object;

            var parser = new RegistrationParser();

            //act
            parser.Parse(ref rawObj, ref outputObj);

            //assert
            Assert.True(expectedOutcome.SequencesEqual(rawInputMoq.Object.FormattedEntryText));
        }
    }
}
