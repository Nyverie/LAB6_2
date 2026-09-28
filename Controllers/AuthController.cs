using LAB5_Fatima.Models;
using LAB5_Fatima.Services;
using LAB5_Fatima.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LAB5_Fatima.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService) => _authService = authService;


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel login)
    {
        var token = await _authService.LoginAsync(login);
        if (token == null) return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
        return Ok(new { token });
    }
    
    [Authorize(Policy = "SoloAdmin")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        var ok = await _authService.RegisterAsync(model);
        if (!ok) return BadRequest(new { mensaje = "El usuario ya existe." });
        return Ok(new { mensaje = "Usuario creado." });
    }

    // Cualquier usuario autenticado
    [Authorize]
    [HttpGet("perfil")]
    public IActionResult Perfil() => Ok(new
    {
        username = User.Identity?.Name,
        role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value
    });

    // Solo Admin
    [Authorize(Policy = "SoloAdmin")]
    [HttpGet("admin")]
    public IActionResult Admin() => Ok("Datos solo para administradores");

    // Solo User
    [Authorize(Policy = "SoloUser")]
    [HttpGet("user")]
    public IActionResult UserData() => Ok("Datos solo para user");

    // Admin o User
    [Authorize(Policy = "AdminOUser")]
    [HttpGet("reportes")]
    public IActionResult Reportes() => Ok("Reportes para Admin y User");
}