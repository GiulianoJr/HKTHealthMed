using System.Text.Json.Serialization;

namespace PROJ_HealthMed.Models
{
    public class Agenda
    {
        public int IdAgenda { get; set; }
        public int MedicoDR { get; set; }
        public DateTime Data { get; set; }
        public int Duracao { get; set; }
        public string Tipo { get; set; }
    }
}
