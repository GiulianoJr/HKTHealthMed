using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IMedico
    {
        Task<int> AddMedico(Medico medico);
        Task<int> UpdateMedico(Medico medico);
        Task<Medico> GetMedicoById(int id);
        Task<Medico> GetMedicoByCRMSenha(string crm, string senha);
        Task<IEnumerable<Medico>> GetMedicos();
        Task<IEnumerable<Medico>> GetMedicosBySpec(string Spec);
        Task<IEnumerable<Agendamento>> GetAgendamentoPendente(int IdMedico);
        Task<int> AceiteAgendamento(int IdAgendamento, bool Aceite);
    }
}
