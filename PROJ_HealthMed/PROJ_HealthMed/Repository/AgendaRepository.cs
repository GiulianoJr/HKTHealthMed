using Dapper;
using System.Data.SqlClient;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using System.Data;
using System.Text.RegularExpressions;

namespace PROJ_HealthMed.Repository
{
    public class AgendaRepository : IAgenda
    {
        private readonly IDbConnection _dbConnection;

        public AgendaRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddAgenda(Agenda agenda)
        {
            var sql = "INSERT INTO Agenda (MedicoDR, Data, Duracao, Tipo) VALUES (@MedicoDR, @Data, @Duracao, @Tipo); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, agenda);
        }

        public async Task<int> UpdateAgenda(Agenda agenda)
        {
            var sql = "UPDATE Agenda SET MedicoDR = @MedicoDR, Data = @Data, Duracao = @Duracao, Tipo = @Tipo WHERE IdAgenda = @IdAgenda";

            return _dbConnection.Execute(sql, agenda);
        }

        public async Task<int> DeleteAgenda(int IdAgenda)
        {
            var sql = "Delete from Agenda where IdAgenda = @IdAgenda";
            return _dbConnection.Execute(sql, new { IdAgenda = IdAgenda });
        }
        public async Task<Agenda> GetAgendaById(int id)
        {
            var sql = "SELECT * FROM Agenda WHERE IdAgenda = @Id";
            return _dbConnection.Query<Agenda>(sql, new { Id = id }).SingleOrDefault();
        }

        public async Task<IEnumerable<Agenda>> GetAgendaByMedico(int IdMedico)
        {
            var sql = "SELECT * FROM Agenda WHERE MedicoDR = @IdMedico";
            return _dbConnection.Query<Agenda>(sql, new { IdMedico = IdMedico });
        }

    }
}
