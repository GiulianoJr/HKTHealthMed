using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Notificacao
    {
        [JsonIgnore]
        public int IdNotificacao { get; set; }
        public int AgendamentoDR { get; set; }
        public string StatusEnvio { get; set; }
        public DateTime DataEnvio { get; set; }
        public TimeSpan HoraEnvio { get; set; }
    }
}
