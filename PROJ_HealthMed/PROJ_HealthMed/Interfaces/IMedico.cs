using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IMedico
    {
        Task<int> AddMedico(Medico medico);
        Task<int> UpdateMedico(Medico medico);
        Task<Medico> GetMedicoById(int id);
        Task<Medico> GetMedicoByEmailSenha(string email, string senha);
        Task<IEnumerable<Medico>> GetMedicos();
    }
}
