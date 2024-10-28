using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarrentlyTheBestAPI.Controllers
{
    [Route("api/opinia")]
    [ApiController]
    public class OpiniaController : ControllerBase
    {
        public readonly IOpiniaService _opiniaService;
        public OpiniaController(IOpiniaService opiniaService)
        {
            _opiniaService = opiniaService;
        }

        [HttpGet("lista")]
        public ActionResult<IEnumerable<Opinia>> GetAll()
        {
            var opinie = _opiniaService.GetAll();
            return Ok(opinie);
        }

        [HttpPost]
        [Authorize]
        public ActionResult UtworzOpinie([FromBody] Opinia opinia)
        {
            var id = _opiniaService.UtworzOpinie(opinia);
            return Created($"/api/wypozyczenie/{id}", null);
        }

        [HttpDelete("{id}")]
        [Authorize]
        public ActionResult UsunOpinie([FromRoute] int id)
        {
            var isDeleted = _opiniaService.DeleteById(id);
            if (isDeleted)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
