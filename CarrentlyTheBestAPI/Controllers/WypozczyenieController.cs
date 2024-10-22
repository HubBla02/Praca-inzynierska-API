using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarrentlyTheBestAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/wypozyczenie")]
    public class WypozyczenieController : ControllerBase
    {
        private readonly IWypozyczenieService _wypozyczenieService;
        public WypozyczenieController(IWypozyczenieService wypozyczenieService)
        {
            _wypozyczenieService = wypozyczenieService;
        }
        [HttpGet("lista")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Wypozyczenie>> GetAll()
        {
            var wypozyczenia = _wypozyczenieService.GetAll();
            return Ok(wypozyczenia);
        }

        [Authorize]
        [HttpGet("moje")]
        public ActionResult<IEnumerable<Wypozyczenie>> GetUserRentals()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;

            if (userEmail == null)
            {
                return Unauthorized("Nie znaleziono emaila w tokenie JWT.");
            }

            var wypozyczenia = _wypozyczenieService.GetByUserEmail(userEmail);

            if (!wypozyczenia.Any())
            {
                return NotFound("Nie znaleziono wypożyczeń dla podanego użytkownika.");
            }

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
