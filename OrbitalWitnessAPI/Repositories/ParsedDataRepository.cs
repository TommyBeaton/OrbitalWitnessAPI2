using Microsoft.EntityFrameworkCore;
using OrbitalWitnessAPI.Context;
using OrbitalWitnessAPI.Domain;
using OrbitalWitnessAPI.Interfaces;

namespace OrbitalWitnessAPI.Repositories
{
    public class ParsedDataRepository : IParsedDataRepository
    {
        private readonly IOrbitalWitnessContext _context;

        public ParsedDataRepository(
            IOrbitalWitnessContext context,
            IParsedScheduleOrmFactory parsedScheduleOrmFactory)
        {
            _context = context;
        }

        public int Create(ParsedSchedule entity)
        {
            _context.ParsedSchedules.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(ParsedSchedule entity)
        {
            _context.ParsedSchedules.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<ParsedSchedule> FindByCondition(Func<ParsedSchedule, bool> expression)
        {
            return _context.ParsedSchedules.AsEnumerable().Where(s => expression(s));
        }

        public ParsedSchedule FindByContent(string content)
        {
            var data = _context.ParsedSchedules
                .Where(x => x.RawData == content)
                .Include(x => x.Notes).AsEnumerable()
                .FirstOrDefault();
                
            return data;
        }

        public ParsedSchedule FindById(int id)
        {
            return _context.ParsedSchedules.AsEnumerable().First(s => s.ParsedDataId == id);
        }

        public IEnumerable<ParsedSchedule> GetAll()
        {
            //Get all 
            //Loop and pull out
            var data = _context.ParsedSchedules
                .Include(c => c.Notes).AsEnumerable();

            return data;
        }

        public int Update(ParsedSchedule entity)
        {
            _context.ParsedSchedules.Update(entity);
            return _context.SaveChanges();
        }
    }
}
