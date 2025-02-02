using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IAgendaRepository
    {
        Task<int> AddAgenda(Agenda agenda);
        Task<Agenda> GetAgendaById(int id);
    }
}
