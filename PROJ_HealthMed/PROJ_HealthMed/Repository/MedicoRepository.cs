using PROJ_HealthMed.Interfaces;
using PROJ_HealthMed.Models;
using System.Data;
using Dapper;

namespace PROJ_HealthMed.Repository
{
    public class MedicoRepository : IMedico
    {
        private readonly IDbConnection _dbConnection;

        public MedicoRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        public async Task<int> AddMedico(Medico medico)
        {
            var sql = "INSERT INTO Medico (Nome, CPF, CRM, Email, Senha) VALUES (@Nome, @CPF, @CRM, @Email, @Senha); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, medico);
        }

        public async Task<int> UpdateMedico(Medico medico)
        {
            var sql = "UPDATE Medico SET Nome = @Nome, CPF = @CPF, CRM = @CRM, Email = @Email, Senha = @Senha WHERE IdUsuario = @IdMedico";

            return _dbConnection.Execute(sql, medico);
        }

        public async Task<Medico> GetMedicoById(int id)
        {
            var sql = "SELECT * FROM Medico WHERE IdMedico = @Id";

            return _dbConnection.Query<Medico>(sql, new { Id = id }).SingleOrDefault();
        }

        public async Task<Medico> GetMedicoByEmailSenha(string email, string senha)
        {
            var sql = "SELECT * FROM Medico WHERE Email = @Email AND Senha = @Senha";
            return _dbConnection.Query<Medico>(sql, new { Email = email, Senha = senha }).SingleOrDefault();
        }
        public async Task<IEnumerable<Medico>> GetMedicos()
        {
            var sql = "SELECT * FROM Medico";

            return _dbConnection.Query<Medico>(sql);
        }
    }
}
