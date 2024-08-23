using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("api/wypozyczenie")]
    public class WypozyczenieController : ControllerBase
    {
        private readonly IWypozyczenieService _wypozyczenieService;
        public WypozyczenieController(IWypozyczenieService wypozyczenieService)
        {
            _wypozyczenieService = wypozyczenieService;
        }
        [HttpGet("lista")]
        public ActionResult<IEnumerable<Wypozyczenie>> GetAll()
        {
            var wypozyczenia = _wypozyczenieService.GetAll();
            return Ok(wypozyczenia);
        }

        [HttpGet("{id}")]
        public ActionResult<Wypozyczenie> GetById([FromRoute] int id)
        {
            var wypozyczenie = _wypozyczenieService.GetById(id);
            if (wypozyczenie == null)
            {
                return NotFound();
            }
            return Ok(wypozyczenie);
        }

        [HttpPost]
        public ActionResult UtworzWypozyczenie([FromBody] Wypozyczenie wypozyczenie)
        {
            var id = _wypozyczenieService.UtworzWypozyczenie(wypozyczenie);
            return Created($"/api/wypozyczenie/{id}", null);
        }

        [HttpDelete("{id}")]
        public ActionResult UsunWypozyczenie([FromRoute] int id)
        {
            var isDeleted = _wypozyczenieService.DeleteById(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
