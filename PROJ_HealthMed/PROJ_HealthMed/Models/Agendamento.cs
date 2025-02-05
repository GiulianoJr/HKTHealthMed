using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Agendamento
    {
        public int IdAgendamento { get; set; }
        public required int AgendaDR { get; set; }
        public required int PacienteDR { get; set; }
        public required int MedicoDR { get; set; }
        public string ?StatusAgendamento { get; set; }
        public string ?MotivoCancelamento { get; set; }
    }
}
