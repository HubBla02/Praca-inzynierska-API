using CarrentlyTheBestAPI.DTO;
using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CarrentlyTheBestAPI.Controllers
{
    [ApiController]
    [Route("api/zgloszenie")]
    [Authorize]
    public class ZgloszenieController : ControllerBase
    {
        private readonly IZgloszenieService _zgloszenieService;
        public ZgloszenieController(IZgloszenieService zgloszenieService)
        {
            _zgloszenieService = zgloszenieService;
        }
        [HttpGet("lista")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Zgloszenie>> GetAll()
        {
            var zgloszenia = _zgloszenieService.GetAll();
            return Ok(zgloszenia);
        }

        [Authorize]
        [HttpGet("moje")]
        public ActionResult<IEnumerable<Zgloszenie>> GetUserReports()
        {
            var userEmail = User.FindFirst(ClaimTypes.Email).Value;

            if (userEmail == null)
            {
                return Unauthorized("Nie znaleziono emaila w tokenie JWT.");
            }

            var zgloszenia = _zgloszenieService.GetByUserEmail(userEmail);

            if (!zgloszenia.Any())
            {
                return NotFound("Nie znaleziono zgłoszeń dla podanego użytkownika.");
            }

            return Ok(zgloszenia);
        }

        [HttpGet("{id}")]
        public ActionResult<Zgloszenie> GetById([FromRoute] int id)
        {
            var zgloszenie = _zgloszenieService.GetById(id);
            if (zgloszenie == null)
            {
                return NotFound();
            }
            return Ok(zgloszenie);
        }

        [HttpPost]
        public ActionResult UtworzZgloszenie([FromBody] Zgloszenie zgloszenie)
        {
            var id = _zgloszenieService.UtworzZgloszenie(zgloszenie);
            return Created($"/api/zgloszenie/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult UsunZgloszenie([FromRoute] int id)
        {
            var isDeleted = _zgloszenieService.UsunZgloszenie(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult ZmienStatus([FromRoute] int id, [FromBody] Zgloszenie status) 
        {
            var result = _zgloszenieService.ZmienStatus(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpPatch("{id}/odpowiedz")]
        [Authorize(Roles = "Admin")]
        public ActionResult Odpowiedz([FromRoute] int id, [FromBody] OdpowiedzDTO odpowiedz)
        {
            var result = _zgloszenieService.DodajOdpowiedz(id, odpowiedz.Odpowiedz);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }
    }
}
