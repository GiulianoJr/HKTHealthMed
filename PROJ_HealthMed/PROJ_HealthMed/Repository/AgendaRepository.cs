using Dapper;
using System.Data.SqlClient;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using System.Data;

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
            var sql = "INSERT INTO Agenda (MedicoDR, Data, Hora, Duracao, Tipo) VALUES (@MedicoDR, @Data, @Hora, @Duracao, @Tipo); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, agenda);
        }

        public async Task<Agenda> GetAgendaById(int id)
        {
            var sql = "SELECT * FROM Agenda WHERE IdAgenda = @Id";
            return _dbConnection.Query<Agenda>(sql, new { Id = id }).SingleOrDefault();
        }
    }
}
