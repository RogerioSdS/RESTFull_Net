using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNETUdemy.Business;
using RestWithASPNETUdemy.Data.VO;
using RestWithASPNETUdemy.Hypermedia.Filters;

namespace RestWithASPNETUdemy.Controllers
{
    [ApiVersion("1")]
    [ApiController]
    [Route("v{version:apiVersion}/api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private IBookBusiness _bookBusiness;

        public BookController(ILogger<PersonController> logger, IBookBusiness bookBusiness)
        {
            _logger = logger;
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookVO>))]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(204)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(400)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(401)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [TypeFilter(typeof(HyperMediaFilter))]//Relativo ao HATEOAS
        public IActionResult Get()
        {
            return Ok(_bookBusiness.FindAll());

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookVO))]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(204)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(400)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(401)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [TypeFilter(typeof(HyperMediaFilter))]//Relativo ao HATEOAS
        public IActionResult Get(long id)
        {
            var person = _bookBusiness.FindById(id);
            if (person == null) return NotFound();
            return Ok(person);
        }

        [HttpPost()]
        [ProducesResponseType(200, Type = typeof(BookVO))]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(400)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(401)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [TypeFilter(typeof(HyperMediaFilter))]//Relativo ao HATEOAS
        public IActionResult Post([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Create(book));
        }

        [HttpPut()]
        [ProducesResponseType(200, Type = typeof(BookVO))]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(400)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(401)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [TypeFilter(typeof(HyperMediaFilter))]//Relativo ao HATEOAS
        public IActionResult Put([FromBody] BookVO book)
        {
            if (book == null) return BadRequest();
            return Ok(_bookBusiness.Update(book));
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(400)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        [ProducesResponseType(401)]//Relativo ao tipo de tratamento que vamos apresentar no Swagger
        public IActionResult Delete(long id)
        {
            _bookBusiness.Delete(id);
            return NoContent();
        }

    }
}
