using Microsoft.AspNetCore.Mvc;
using VueAppWithAspNetApi.Server.Application.DTOs;
using VueAppWithAspNetApi.Server.Application.Interfaces;
using VueAppWithAspNetApiServer.Domain.Entities;
using VueAppWithAspNetApi.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace VueAppWithAspNetApi.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<AuthController> _logger;

        public AuthController(AppDbContext context, ILogger<AuthController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            try
            {
                _logger.LogInformation("Register isteği alındı: {@User}", user);

                if (await _context.Users.AnyAsync(x => x.Email == user.Email))
                {
                    _logger.LogWarning("Email zaten kullanımda: {Email}", user.Email);
                    return BadRequest("Bu email adresi zaten kullanımda");
                }

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                _logger.LogInformation("Kullanıcı başarıyla kaydedildi: {Email}", user.Email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Kayıt işlemi sırasında hata oluştu");
                return StatusCode(500, "Kayıt işlemi sırasında bir hata oluştu");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                _logger.LogInformation("Login isteği alındı: {Email}", model.Email);

                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.Email == model.Email && x.Password == model.Password);

                if (user == null)
                {
                    _logger.LogWarning("Geçersiz giriş denemesi: {Email}", model.Email);
                    return BadRequest("Email veya şifre hatalı");
                }

                _logger.LogInformation("Kullanıcı başarıyla giriş yaptı: {Email}", model.Email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Giriş işlemi sırasında hata oluştu");
                return StatusCode(500, "Giriş işlemi sırasında bir hata oluştu");
            }
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
} 