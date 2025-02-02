using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PROJ_HealthMed.Controllers;

[Route("api/[controller]")]
[ApiController]

public class UsuarioController : ControllerBase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly TokenService _tokenService;

    public UsuarioController(IUsuarioRepository usuarioRepository, TokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    [HttpPost("salvar")]
    public async Task<IActionResult> SaveUsuario([FromBody] Usuario usuario)
    {
        var id = await _usuarioRepository.AddUsuario(usuario);
        return CreatedAtAction(nameof(GetUsuarioById), new { id }, usuario);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUsuario(int id, [FromBody] Usuario usuario)
    {
        usuario.IdUsuario = id;
        var rowsAffected = await _usuarioRepository.UpdateUsuario(usuario);
        if (rowsAffected == 0)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
    {
        var usuario = await _usuarioRepository.GetUsuarioById(id);

        if (usuario == null)
        {
            return NotFound();
        }

        return usuario;
    }

    [HttpPost("logar")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var usuario = await _usuarioRepository.GetUsuarioByEmailSenha(loginRequest.Email, loginRequest.Senha);

        if (usuario == null)
        {
            return Unauthorized();
        }

        var token = _tokenService.GenerateToken(usuario.Email);
        return Ok(new { Token = token });
    }
}

public class LoginRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
}
