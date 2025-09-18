using IntegerCollectionAPI.models;
using Microsoft.AspNetCore.Mvc;

namespace IntegerCollectionAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class IntegerCollectionController : ControllerBase
    {
        private IDataRepository dataRepository;

        public IntegerCollectionController(IDataRepository dataRepository)
        {
            this.dataRepository = dataRepository;
        }


        //return the numbers stored in repository
        [HttpGet("numbers")]
        public ActionResult<List<int>> GetNumbers()
        {
            return Ok(dataRepository.GetNumbers());
        }

        //returns the number
        //sorted in ascending or descending order
        [HttpGet("numbers/sorted")]
        public ActionResult<List<int>> GetNumbersSorted(string sort)
        {
            List<int> numbersSorted;
            if (sort.Equals("asc")) {
                numbersSorted = dataRepository.GetSorted(true);
            }
            else if (sort.Equals("desc")) {
                numbersSorted = dataRepository.GetSorted(false);
            }
            else
            {
                return BadRequest();
            }
            return Ok(numbersSorted);
        }

        //searches for the value in repository
        [HttpGet("numbers/search")]
        public ActionResult<SearchResult> SearchForValue(int value)
        {
            bool valueFound = dataRepository.FindNumber(value);
            return Ok(new SearchResult(value, valueFound));
          
        }

        //returns basic statistics about data in repository
        [HttpGet("numbers/stats")]
        public ActionResult<Statistics> GetStatistics()
        {
            if(dataRepository.GetNumbers().Count() == 0)
            {
                return BadRequest();
            }
            return Ok(new Statistics(dataRepository.Mean(), dataRepository.Median()));
        }

        //adds data to repository
        [HttpPost("numbers")]
        public ActionResult PostNumbers([FromBody] List<int> numbersPosted)
        {
            dataRepository.AddNumbers(numbersPosted);
            return Ok();
        }

      
       
    }

}
