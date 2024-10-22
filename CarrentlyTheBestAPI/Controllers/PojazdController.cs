using CarrentlyTheBestAPI.DTO;
using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace CarrentlyTheBestAPI.Controllers
{
    [ApiController]
    [Route("api/pojazd")]
    [Authorize]
    public class PojazdController : ControllerBase
    {
        private readonly IPojazdService _pojazdService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;

        public PojazdController(IPojazdService pojazdService, IWebHostEnvironment webHostEnvironment, IFileService fileService)
        {
            _pojazdService = pojazdService;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
        }
        [HttpGet("lista")]
        public ActionResult<IEnumerable<Pojazd>> GetAll()
        {
            var pojazdy = _pojazdService.GetAll();
            return Ok(pojazdy);
        }

        [HttpGet("{id}")]
        public ActionResult<Pojazd> GetPojazdById([FromRoute] int id)
        {
            var pojazd = _pojazdService.GetById(id);
            return pojazd;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult UtworzPojazd([FromBody] PojazdDTO model)
        {
            if (ModelState.IsValid)
            {
                var pojazd = new Pojazd();

                pojazd.Typ = model.Typ;
                pojazd.Marka = model.Marka;
                pojazd.Model = model.Model;
                pojazd.RodzajSkrzyni = model.RodzajSkrzyni;
                pojazd.RodzajPaliwa = model.RodzajPaliwa;
                pojazd.RokProdukcji = model.RokProdukcji;
                pojazd.Dostepny = true;
                pojazd.CenaD = model.CenaD;
                pojazd.CenaK = model.CenaK;
                var id = _pojazdService.UtworzPojazd(pojazd);
                return Created($"/api/pojazd/{id}", null);
            }

            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { Errors = errors });
        }

        [HttpPost("{id}/upload-zdjecie")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = "Admin")]
        public ActionResult UploadZdjecie([FromRoute] int id, [FromForm] IFormFile plik)
        {
            if (plik == null)
                return BadRequest("Nie wybrano pliku");

            var result = _fileService.ZapiszZdjecie(plik);
            if (result.Item1 == 1)
            {
                var success = _pojazdService.DodajZdjecie(id, result.Item2);
                if (success)
                    return Ok(new { Sciezka = result.Item2 });

                return NotFound("Pojazd nie znaleziony");
            }

            return StatusCode(500, "Wystąpił błąd podczas zapisywania zdjęcia");
        }


        [HttpDelete("{id}/usun-zdjecie")]
        [Authorize(Roles = "Admin")]
        public ActionResult UsunZdjecie([FromRoute] int id)
        {
            var pojazd = _pojazdService.GetById(id);
            if (pojazd == null || string.IsNullOrEmpty(pojazd.SciezkaDoZdjecia))
            {
                return NotFound("Pojazd lub zdjęcie nie znaleziono.");
            }

            var success = _fileService.UsunZdjecie(pojazd.SciezkaDoZdjecia);
            if (success)
            {
                var notNulled = _pojazdService.UsunZdjecie(pojazd.Id);
                if (notNulled)
                {
                    return NoContent();
                }
                return StatusCode(500, "Nie udało się zmienić ścieżki");
            }

            return StatusCode(500, "Wystąpił błąd podczas usuwania zdjęcia.");
        }




        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public  ActionResult EdytujPojazd([FromRoute] int id, PojazdDTO zmiany)
        {
            var result = _pojazdService.EdytujPojazd(id, zmiany);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
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
