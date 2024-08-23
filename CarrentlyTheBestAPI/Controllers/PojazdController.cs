using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Controllers
{
    [ApiController]
    [Route("api/pojazd")]
    //[Authorize]
    public class PojazdController : ControllerBase
    {
        private readonly IPojazdService _pojazdService;
        public PojazdController(IPojazdService pojazdService)
        {
            _pojazdService = pojazdService;
        }
        [HttpGet("lista")]
        public ActionResult<IEnumerable<Pojazd>> GetAll()
        {
            var pojazdy = _pojazdService.GetAll();
            return Ok(pojazdy);
        }

        [HttpGet("{id}")]
        public ActionResult<Pojazd> GetById([FromRoute] int id)
        {
            var pojazd = _pojazdService.GetById(id);
            if (pojazd == null)
            {
                return NotFound();
            }
            return Ok(pojazd);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public ActionResult UtworzPojazd([FromBody] Pojazd pojazd, IFormFile zdjecie)
        {
            var id = _pojazdService.UtworzPojazd(pojazd);
            return Created($"/api/pojazd/{id}", null);
        }

        [HttpPatch("{id}")]
        //[Authorize(Roles = "Admin")]
        public  ActionResult EdytujPojazd([FromRoute] int id, Pojazd zmiany, IFormFile zdjecie)
        {
            var result = _pojazdService.EdytujPojazd(id, zmiany);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        public ActionResult UsunPojazd([FromRoute] int id)
        {
            var isDeleted = _pojazdService.DeleteById(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
