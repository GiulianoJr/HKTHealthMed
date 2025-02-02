using PROJ_HealthMed.Interfaces;
using PROJ_HealthMed.Models;
using System.Data;
using Dapper;

namespace PROJ_HealthMed.Repository
{
    public class PacienteRepository : IPaciente
    {
        private readonly IDbConnection _dbConnection;
        public PacienteRepository(IDbConnection dbConnection)
        {
           _dbConnection = dbConnection;
        }
        public async Task<int> AddPaciente(Paciente paciente)
        {
            var sql = "INSERT INTO Paciente (Nome, CPF, Email, Senha) VALUES (@Nome, @CPF, @Email, @Senha); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, paciente);
        }

        public async Task<int> UpdatePaciente(Paciente paciente)
        {
            var sql = "UPDATE Medico SET Nome = @Nome, CPF = @CPF, Email = @Email, Senha = @Senha WHERE IdUsuario = @IdMedico";

            return _dbConnection.Execute(sql, paciente);
        }

        public async Task<Paciente> GetPacienteByEmailSenha(string email, string senha)
        {
            var sql = "SELECT * FROM Paciente WHERE Email = @Email AND Senha = @Senha";
            return _dbConnection.Query<Paciente>(sql, new { Email = email, Senha = senha }).SingleOrDefault();

        }

        public async Task<Paciente> GetPacienteById(int id)
        {
            var sql = "SELECT * FROM Paciente WHERE IdPaciente = @Id";

            return _dbConnection.Query<Paciente>(sql, new { Id = id }).SingleOrDefault();
        }

        public async Task<IEnumerable<Paciente>> GetPacientes()
        {
            var sql = "SELECT * FROM Paciente";

            return _dbConnection.Query<Paciente>(sql);
        }
    }
}
