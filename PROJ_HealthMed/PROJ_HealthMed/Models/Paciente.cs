using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Paciente
    {
        public int IdPaciente { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
