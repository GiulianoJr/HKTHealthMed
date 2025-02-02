using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<int> AddUsuario(Usuario usuario);
        Task<int> UpdateUsuario(Usuario usuario);
        Task<Usuario> GetUsuarioById(int id);
        Task<Usuario> GetUsuarioByEmailSenha(string email, string senha);
        Task<IEnumerable<Usuario>> GetMedicos();
    }
}