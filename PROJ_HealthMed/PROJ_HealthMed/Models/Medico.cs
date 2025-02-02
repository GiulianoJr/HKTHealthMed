using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Medico
    {
        [JsonIgnore]
        public int IdMedico { get; set; }
        public required string Nome { get; set; }
        public required string CPF { get; set; }
        public required string CRM { get; set; }
        public required string Email { get; set; }
        public required string Senha { get; set; }
    }
}
