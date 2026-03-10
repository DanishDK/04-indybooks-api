using Microsoft.AspNetCore.Mvc;
using IndyBooks.Models;
using IndyBooks.Services;

namespace IndyBooks.Controllers
{
    [Route("api/")] //The base route for all calls to this controller
    public class ApiController : Controller
    {
        private IWriterService _writerService;
        public ApiController(IWriterService writerService) { _writerService = writerService; }

        /**
         * READ ALL: Retrieves a collection of writers
         * uses a GET verb with the URL pattern "api/writers"
         */
        [Route("writers/")]
        [HttpGet]
        public IActionResult GetWriters()
        {
            return Ok(_writerService.GetWriterList());
        }
         /**
         * READ ONE: Retrieves a particular writer with the given {id}
         * uses a GET verb with the URL pattern "api/writers/41"
         */
        [Route("writers/{id}")]//This annotation passes along the Route Id for the api
        [HttpGet] 
        public IActionResult GetWriter(long id)
        {
            if(_writerService.GetWriterById(id) == null) //DONE: Test for missing record using the lambda Extension method .Any()
            {
                return NotFound(); //DONE: if not return NotFound() instead of Ok()
            }

        //Otherwise return Ok(writer);
            return Ok( _writerService.GetWriterById(id) );
        }

        /**
         * DELETE: Removes a particular writer with the given {id}
         * uses a DELETE verb with the URL pattern "api/writers/37"
         */
        [Route("writers/{id}")]
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            //TODO: Udpate the  for record using the _writerService.GetWritersList and the Any() collections method
            if (_writerService.GetWriterById(id) ==null) 
            { 
                return NotFound(); 
            }

            //TODO: Pass the _writerService DeleteWriterById method to Accepted() below

            return Accepted(_writerService.DeleteWriterById(id));
        }
        /**
         * CREATE: Add a new writer to the collection
         * uses a POST verb with the URL pattern "api/writers"
         */
        [Route("writers/")]
        [HttpPost]
        public IActionResult PostWriter([FromBody]Writer writer)
        {
            //TODO: Test for an invalid ModelState -> return BadRequest();


            //TODO: Pass the result from the _writerService PostWriter method to Accepted() below

            return Accepted( 0 );

        }
       
        /** 
         * UPDATE: Modify a given writer with id and [FromBody]Writer writer parameters
         * uses a PUT verb with the URL pattern "api/writers/16"
         */
        [Route("writers/{id}")]
        [HttpPut] 
        public IActionResult PutWriter([FromBody]Writer writer, long id)
        {
        //TODO: Test for an invalid ModelState -> return BadRequest();


        //TODO: Test for missing record using Any() -> return NotFound();


        //TODO: Otherwise, pass the results of the _writerService PutWriter method to Accepted() below

            return Accepted( new Writer{ Name = "Replace me with the results of the PutWriter method in WriterService"} );
        }
    }
}

