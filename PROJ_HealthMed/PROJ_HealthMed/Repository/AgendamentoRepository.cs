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
            agendamento.StatusAgendamento = "Pendente";
            var sql = "INSERT INTO Agendamento (AgendaDR, PacienteDR, MedicoDR, StatusAgendamento) VALUES (@AgendaDR, @PacienteDR, @MedicoDR, @StatusAgendamento); SELECT CAST(SCOPE_IDENTITY() as int);";

            return _dbConnection.Execute(sql,agendamento);
            
      }

        public async Task<Agendamento> GetAgendamentoById(int id)
        {
            var sql = "SELECT * FROM Agendamento WHERE IdAgendamento = @Id";
            return _dbConnection.Query<Agendamento>(sql, new { Id = id }).SingleOrDefault();
        }

        public async Task<int> CancelarAgendamento(int IdAgendamento, string mtvCanc)
        {
            var sqlA = "SELECT * FROM Agendamento WHERE IdAgendamento = @Id";
            Agendamento agendamento = _dbConnection.Query<Agendamento>(sqlA, new { Id = IdAgendamento }).SingleOrDefault();

            int IdAgenda = agendamento.AgendaDR;

            var sqlB = "update Agenda set StatusAgenda = 'Disponivel' where IdAgenda = @IdAgenda";
            var ok = _dbConnection.Execute(sqlB, new { mtvCanc = mtvCanc, IdAgenda = IdAgenda});
          
            var sql = "UPDATE Agendamento Set MotivoCancelamento = @mtvCanc, StatusAgendamento = 'Cancelado' where IdAgendamento = @IdAgendamento";
            return _dbConnection.Execute(sql, new { mtvCanc= mtvCanc, IdAgendamento = IdAgendamento });
        }
    }
}
