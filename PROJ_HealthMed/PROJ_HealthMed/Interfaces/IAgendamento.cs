using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IAgendamento
    {
        Task<int> AddAgendamento(Agendamento agendamento);
        Task<Agendamento> GetAgendamentoById(int id);
    }
}
