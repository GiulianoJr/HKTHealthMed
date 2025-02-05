using Microsoft.AspNetCore.Mvc;
using PROJ_HealthMed.Interfaces;
using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : Controller
    {
        private readonly IPaciente _PacienteInterface;
        private readonly TokenService _tokenService;

        public PacienteController(IPaciente pacienteInterface, TokenService tokenService)
        {
            _PacienteInterface = pacienteInterface;
            _tokenService = tokenService;
        }

        [HttpPost("Salvar")]
        public async Task<IActionResult> SavePaciente([FromBody] Paciente paciente)
        {
            var id = await _PacienteInterface.AddPaciente(paciente);
            return CreatedAtAction(nameof(GetPacienteById), new { id }, paciente);
        }

        [HttpGet("ConsultarPorId/{id}")]
        public async Task<ActionResult<Paciente>> GetPacienteById(int id)
        {
            var paciente = await _PacienteInterface.GetPacienteById(id);

            if (paciente == null)
            {
                return NotFound();
            }

            return paciente;
        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<Paciente>>> GetPacientes()
        {
            var pacientes = await _PacienteInterface.GetPacientes();
            return Ok(pacientes);
        }

        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> UpdatePaciente(int id, [FromBody] Paciente paciente)
        {
            paciente.IdPaciente = id;
            var rowsAffected = await _PacienteInterface.UpdatePaciente(paciente);
            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginPacienteRequest loginPacienteRequest)
        {
            if ((loginPacienteRequest.CPF==null) & (loginPacienteRequest.Email == null))
            {
                return BadRequest();
            }

            var paciente = await _PacienteInterface.GetPacienteByEmailCPFSenha(loginPacienteRequest.Email, loginPacienteRequest.Senha, loginPacienteRequest.CPF);

            if (paciente == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateTokenPaciente(paciente.Email);
            return Ok(new { Token = token });
        }

        public class LoginPacienteRequest
        {
            public string ?Email { get; set; }
            public string ?CPF { get; set; }
            public required string Senha { get; set; }
        }

    }
}
