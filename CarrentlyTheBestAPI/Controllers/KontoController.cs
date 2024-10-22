using CarrentlyTheBestAPI.Entities;
using CarrentlyTheBestAPI.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarrentlyTheBestAPI.Controllers

{
    [ApiController]
    [Route("api/konto")]
    public class KontoController : ControllerBase
    {
        private readonly IKontoService _kontoService;
        public KontoController(IKontoService kontoService)
        {
            _kontoService = kontoService;
        }
        [HttpPost("register")]
        public ActionResult Rejestracja([FromBody] Rejestracja uzytkownik)
        {
            _kontoService.RejestracjaU(uzytkownik);
            return Ok();
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] Login login)
        {
            try
            {
                string token = _kontoService.generujJWT(login);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }

    }
}
