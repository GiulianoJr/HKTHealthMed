using PROJ_HealthMed.Models;

namespace PROJ_HealthMed.Interfaces
{
    public interface INotificacao
    {
        Task<int> AddNotificacao(Notificacao notificacao);
        Task<Notificacao> GetNotificacaoById(int id);
    }
}
