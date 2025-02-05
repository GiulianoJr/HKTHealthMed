using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IAgenda
    {
        Task<int> AddAgenda(Agenda agenda);
        Task<Agenda> GetAgendaById(int id);
        Task<IEnumerable<Agenda>> GetAgendaByMedico(int IdMedico);
        Task<int> DeleteAgenda(int IdAgenda);
        Task<int> UpdateAgenda(Agenda agenda);
    }
}
