using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IPaciente
    {
        Task<int> AddPaciente(Paciente paciente);
        Task<int> UpdatePaciente(Paciente paciente);
        Task<Paciente> GetPacienteById(int id);
        Task<Paciente> GetPacienteByEmailSenha(string email, string senha);
        Task<IEnumerable<Paciente>> GetPacientes();
    }
}
