using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.Factories
{
    public class ParsedScheduleOrmFactory : IParsedScheduleOrmFactory
    {
        public ParsedSchedule Create(List<string> rawContent, IParsedScheduleNoticeOfLease data)
        {
            string formattedResponse = string.Join("", rawContent);
            var parsedData = new ParsedSchedule
            {
                RawData = formattedResponse,
                EntryNumber = data.EntryNumber,
                EntryDate = data.EntryDate,
                RegistrationDateAndPlanRef = data.RegistrationDateAndPlanRef,
                PropertyDescription = data.PropertyDescription,
                DateOfLeaseAndTerm = data.DateOfLeaseAndTerm,
                LesseesTitle = data.LesseesTitle,
            };

            foreach (var note in data.Notes)
            {
                parsedData.Notes.Add(new Note
                {
                    Content = note
                });
            }

            return parsedData;
        }
    }
}
