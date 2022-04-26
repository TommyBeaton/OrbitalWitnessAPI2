using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.Factories
{
    public class ParsedScheduleDtoFactory : IParsedScheduleDtoFactory
    {
        public IParsedScheduleNoticeOfLease Create(ParsedSchedule data)
        {
            return new ParsedScheduleNoticeOfLease
            {
                EntryNumber = data.EntryNumber,
                EntryDate = data?.EntryDate,
                RegistrationDateAndPlanRef = data.RegistrationDateAndPlanRef,
                PropertyDescription = data.PropertyDescription,
                DateOfLeaseAndTerm = data.DateOfLeaseAndTerm,
                LesseesTitle = data.LesseesTitle,
                Notes = data.Notes.Select(x => x.Content).ToList()
            };
        }
    }
}
