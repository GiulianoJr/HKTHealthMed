using Microsoft.AspNetCore.Mvc;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgendaController : ControllerBase
    {
        private readonly IAgenda _agendaRepository;

        public AgendaController(IAgenda agendaRepository)
        {
            _agendaRepository = agendaRepository;
        }

        [HttpPost("salvar")]
        public async Task<IActionResult> SaveAgenda([FromBody] Agenda agenda)
        {
            var id = await _agendaRepository.AddAgenda(agenda);
            return CreatedAtAction(nameof(GetAgendaById), new { id }, agenda);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Agenda>> GetAgendaById(int id)
        {
            var agenda = await _agendaRepository.GetAgendaById(id);

            if (agenda == null)
            {
                return NotFound();
            }

            return agenda;
        }
    }
}
