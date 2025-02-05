using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendamentoController : ControllerBase
    {
        private readonly IAgendamento _agendamentoRepository;

        public AgendamentoController(IAgendamento agendamentoRepository)
        {
            _agendamentoRepository = agendamentoRepository;
        }

        [HttpPost("salvar")]
        public async Task<IActionResult> SaveAgendamento([FromBody] Agendamento agendamento)
        {
            var id = await _agendamentoRepository.AddAgendamento(agendamento);
            return CreatedAtAction(nameof(GetAgendamentoById), new { id }, agendamento);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agendamento>> GetAgendamentoById(int id)
        {
            var agendamento = await _agendamentoRepository.GetAgendamentoById(id);

            if (agendamento == null)
            {
                return NotFound();
            }

            return agendamento;
        }

        [HttpPut("Cancelar/{IdAgendamento}")]

        public async Task<IActionResult> CancelarAgendamento(int IdAgendamento, [FromBody] MtvCanc mtvCanc)
        {
            var agendamento = await _agendamentoRepository.CancelarAgendamento(IdAgendamento, mtvCanc.MotivoCancelamento);
            return Ok();  
        }
    }

    public class MtvCanc
    {
        public required string MotivoCancelamento { get; set; }
    }

}
