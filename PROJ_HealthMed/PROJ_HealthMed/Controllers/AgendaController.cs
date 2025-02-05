using Microsoft.AspNetCore.Mvc;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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

        [HttpPost("/lista/salvar")]
        public async Task<IActionResult> SaveAgenda([FromBody] List<Agenda> ListaAgenda)
        {
            foreach (var agenda in ListaAgenda)
            {
                var id = await _agendaRepository.AddAgenda(agenda);
            }
            return Ok("Agenda criada com sucesso."); ; //CreatedAtAction(nameof(GetAgendaById), new { id }, agenda);
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


        [HttpGet("BuscarPorMedico/{idMedico}")]
        public async Task<ActionResult<IEnumerable<Agenda>>> GetAgendaByMedico(int idMedico)
        {
            var agenda = await _agendaRepository.GetAgendaByMedico(idMedico);

            if (agenda == null)
            {
                return NotFound();
            }
            
            return Ok(agenda);
        }

    

        [HttpDelete("Deletar/{IdAgenda}")]
        public async Task<IActionResult> DeletarAgenda(int IdAgenda)
        {
            var delete = _agendaRepository.DeleteAgenda(IdAgenda);
            return Ok();
        }

        [HttpPut("Atualizar/{id}")]
        public async Task<IActionResult> UpdateAgenda(int id, [FromBody] Agenda agenda)
        {
            agenda.IdAgenda = id;
            var rowsAffected = await _agendaRepository.UpdateAgenda(agenda );
            if (rowsAffected == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
