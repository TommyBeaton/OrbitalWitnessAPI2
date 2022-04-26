using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrbitalWitnessAPI.DTO;
using OrbitalWitnessAPI.Interfaces;
using System.Net;

namespace OrbitalWitnessAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleDataController : ControllerBase
    {
        private readonly IParsedDataRepository _repository;
        private readonly IParsedScheduleDtoFactory _parsedDtoFactory  ;
        private readonly IOWLegacyApiWrapper _oritalWitnessAPI;
        private readonly IScheduleParser _scheduleParser;
        private readonly IParsedScheduleOrmFactory _scheduleOrmFactory;

        public ScheduleDataController(
            IParsedDataRepository repository,
            IParsedScheduleDtoFactory scheduleDtoFactory,
            IOWLegacyApiWrapper oritalWitnessAPI,
            IScheduleParser scheduleParser,
            IParsedScheduleOrmFactory scheduleOrmFactory)
        {
            _repository = repository;
            _parsedDtoFactory = scheduleDtoFactory;
            _oritalWitnessAPI = oritalWitnessAPI;
            _scheduleParser = scheduleParser;
            _scheduleOrmFactory = scheduleOrmFactory;
        }

        private IParsedScheduleNoticeOfLease GetParsedDataFromDb(List<string> rawData)
        {
            //Parse list to string for easier db storage/checking
            string formattedResponse = String.Join("", rawData);

            var resp = _repository.FindByContent(formattedResponse);
            
            return resp == null ? null : _parsedDtoFactory.Create(resp);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IParsedScheduleNoticeOfLease>>> Get() 
        {
            //make api call to their db
            IList<IParsedScheduleNoticeOfLease> output = new List<IParsedScheduleNoticeOfLease>();
            IList<RawScheduleNoticeOfLease> result;

            try
            {
                //Consume legacy api
                result = _oritalWitnessAPI.GetSchedules().Result;
            }
            catch (Exception ex)
            {
                //If an exception was thrown
                //Let the user know something happened on our side
                return StatusCode(204, ex.Message);
            }

            //Loop through all responses from the API
            foreach(var rawData in result)
            {
                //Check if this data has already been parsed
                var dbResponse = GetParsedDataFromDb(rawData.EntryText);
                
                if(dbResponse != null)
                {
                    //If it has been parsed, add the result to the output
                    output.Add(dbResponse);
                    continue;
                }
                else
                {
                    //Manually parse the data
                    var parsedResponse = _scheduleParser.Parse(rawData);
                    //Add it to the output
                    output.Add(parsedResponse);
                    //Add it to the database
                    _repository.Create(_scheduleOrmFactory.Create(rawData.EntryText, parsedResponse));
                }
            }

            //Return ok response
            return Ok(output);
        }

    }
}
