using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public MedicoController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("buscar")]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetMedicos()
        {
            var medicos = await _usuarioRepository.GetMedicos();
            return Ok(medicos);
        }
    }
}
