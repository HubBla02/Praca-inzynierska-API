using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CarrentlyTheBestAPI.Controllers
{
    [ApiController]
    [Route("api/uzytkownik")]
    [Authorize]
    public class UzytkownikController : ControllerBase
    {
        private readonly IUzytkownikService _uzytkownikService;
        public UzytkownikController(IUzytkownikService uzytkownikService)
        {
            _uzytkownikService = uzytkownikService;
        }
        [HttpGet("lista")]
        [Authorize(Roles = "Admin")]
        public ActionResult<IEnumerable<Uzytkownik>> GetAll()
        {
            var uzytkownicy = _uzytkownikService.GetAll();
            return Ok(uzytkownicy);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult<Uzytkownik> GetById([FromRoute] int id)
        {
            var uzytkownik = _uzytkownikService.GetById(id);
            if (uzytkownik == null)
            {
                return NotFound();
            }
            return Ok(uzytkownik);
        }

        [HttpGet("userByEmail/{email}")]
        public ActionResult<Uzytkownik> GetByEmail([FromRoute] string email)
        {
            var uzytkownik = _uzytkownikService.GetByEmail(email);

            if (uzytkownik == null)
            {
                return NotFound("Nie znaleziono użytkownika o takim adresie email.");
            }

            return Ok(uzytkownik);
        }

        [HttpPatch("{id}")]
        public ActionResult EdytujUzytkownika([FromRoute] int id, Uzytkownik zmiany)
        {
            var result = _uzytkownikService.EdytujUzytkownika(id, zmiany);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public ActionResult UsunUzytkownika([FromRoute] int id)
        {
            var isDeleted = _uzytkownikService.DeleteById(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
