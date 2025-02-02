using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Agendamento
    {
        [JsonIgnore]
        public int IdAgendamento { get; set; }
        public int AgendaDR { get; set; }
        public int PacienteDR { get; set; }
        public string StatusAgendamento { get; set; }
    }
}
