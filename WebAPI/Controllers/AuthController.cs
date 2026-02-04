using Business.Abstract;
using Business.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDto dto)
        {
            var result = await _userService.Register(dto);
            if (result) return Ok("Kullanıcı oluşturuldu.");
            return BadRequest("Kullanıcı adı zaten alınmış.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRegisterDto dto)
        {
            var user = await _userService.Login(dto.Username, dto.Password);
            if (user == null) return Unauthorized("Hatalı kullanıcı adı veya şifre.");

            return Ok(new { user.UserId, user.Username }); // Başarılı giriş
        }
    }
}
