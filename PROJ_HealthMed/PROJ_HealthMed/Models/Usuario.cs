using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Usuario
    {
        [JsonIgnore]
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string TipoUsuario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
    }
}
