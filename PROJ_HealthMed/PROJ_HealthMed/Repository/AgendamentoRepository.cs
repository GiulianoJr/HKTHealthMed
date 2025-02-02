using Dapper;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using System.Data;

namespace PROJ_HealthMed.Repository
{
    public class AgendamentoRepository : IAgendamento
    {
        private readonly IDbConnection _dbConnection;

        public AgendamentoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddAgendamento(Agendamento agendamento)
        {
            var sql = "INSERT INTO Agendamento (AgendaDR, PacienteDR, StatusAgendamento) VALUES (@AgendaDR, @PacienteDR, @StatusAgendamento); SELECT CAST(SCOPE_IDENTITY() as int);";

            return _dbConnection.Execute(sql,agendamento);
            
      }

        public async Task<Agendamento> GetAgendamentoById(int id)
        {
            var sql = "SELECT * FROM Agendamento WHERE IdAgendamento = @Id";
            return _dbConnection.Query<Agendamento>(sql, new { Id = id }).SingleOrDefault();
        }
    }
}
