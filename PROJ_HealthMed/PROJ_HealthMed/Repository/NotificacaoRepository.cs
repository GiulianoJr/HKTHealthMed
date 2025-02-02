using Dapper;
using PROJ_HealthMed.Models;
using System.Data.SqlClient;
using PROJ_HealthMed.Interfaces;
using System.Data;

namespace PROJ_HealthMed.Repository
{
    public class NotificacaoRepository : INotificacaoRepository
    {
        private readonly IDbConnection _dbConnection;

        public NotificacaoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddNotificacao(Notificacao Notificacao)
        {
            var sql = "INSERT INTO Notificacao (AgendamentoDR, StatusEnvio, DataEnvio, HoraEnvio) VALUES (@AgendamentoDR, @StatusEnvio, @DataEnvio, @HoraEnvio); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, Notificacao);
        
        }

        public async Task<Notificacao> GetNotificacaoById(int id)
        {
            var sql = "SELECT * FROM Notificacao WHERE IdNotificacao = @Id";

            return _dbConnection.Query<Notificacao>(sql, new { Id = id }).SingleOrDefault();
        }
    }
}
