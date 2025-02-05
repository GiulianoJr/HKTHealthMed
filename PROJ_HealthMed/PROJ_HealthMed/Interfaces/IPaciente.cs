using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IPaciente
    {
        Task<int> AddPaciente(Paciente paciente);
        Task<int> UpdatePaciente(Paciente paciente);
        Task<Paciente> GetPacienteById(int id);
        Task<Paciente> GetPacienteByEmailCPFSenha(string email, string senha, string CPF);
        Task<IEnumerable<Paciente>> GetPacientes();
    }
}
