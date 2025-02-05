using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using Microsoft.AspNetCore.Mvc;
using PROJ_HealthMed.Repository;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IMedico _MedicoInterface;
        private readonly TokenService _tokenService;

        public MedicoController(IMedico medicoInterface, TokenService tokenService)
        {
            _MedicoInterface = medicoInterface;
            _tokenService = tokenService;
        }

        [HttpPost("Salvar")]
        public async Task<IActionResult> SaveMedico([FromBody] Medico medico)
        {
            var id = await _MedicoInterface.AddMedico(medico);
            return CreatedAtAction(nameof(GetMedicoById), new { id }, medico);
        }

        [HttpGet("ConsultarPorId/{id}")]
        public async Task<ActionResult<Medico>> GetMedicoById(int id)
        {
            var medico = await _MedicoInterface.GetMedicoById(id);

            if (medico == null)
            {
                return NotFound();
            }

            return medico;
        }

        [HttpGet("BuscarTodos")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos()
        {
            var medicos = await _MedicoInterface.GetMedicos();
            return Ok(medicos);
        }

        [HttpGet("BuscarPorEspecialidade/{Especialidade}")]
        public async Task<ActionResult<IEnumerable<Medico>>> GetMedicos(string Especialidade)
        {
            var medicos = await _MedicoInterface.GetMedicosBySpec(Especialidade);
            return Ok(medicos);
        }

        [HttpGet("BuscarAgedamentoPendente/{IdMedico}")]
        public async Task<ActionResult<IEnumerable<Agendamento>>> GetAgendamentoPendente(int IdMedico)
        {
            var agendamentos = await _MedicoInterface.GetAgendamentoPendente(IdMedico);
            return Ok(agendamentos);
        }

        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> UpdateMedico(int id, [FromBody] Medico medico)
        {
            medico.IdMedico = id;
            var rowsAffected = await _MedicoInterface.UpdateMedico(medico);
            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginMedicoRequest loginMedicoRequest)
        {
            var medico = await _MedicoInterface.GetMedicoByCRMSenha(loginMedicoRequest.CRM, loginMedicoRequest.Senha);

            if (medico == null)
            {
                return Unauthorized();
            }

            var token = _tokenService.GenerateToken(medico.CRM);
            return Ok(new { Token = token });
        }

        [HttpPut("AceiteAgendamento")]
        public async Task<IActionResult> AceiteAgendamento([FromBody] AceiteAgendamento AceiteAgendamento)
        {
            var ok = _MedicoInterface.AceiteAgendamento(AceiteAgendamento.IdAgendamento, AceiteAgendamento.Aceite);
            return Ok();
        }
    }
}
public class LoginMedicoRequest
{
    public required string CRM { get; set; }
    public required string Senha { get; set; }
}

public class AceiteAgendamento
{
    public required int IdAgendamento { get; set; }

    public required bool Aceite { get; set; }
}

