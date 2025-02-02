using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Notificacao
    {
        [JsonIgnore]
        public int IdNotificacao { get; set; }
        public required int AgendamentoDR { get; set; }
        public required string StatusEnvio { get; set; }
        public required int MedicoDR { get; set; }
        public DateTime ?DataHoraEnvio { get; set; }
    }
}
