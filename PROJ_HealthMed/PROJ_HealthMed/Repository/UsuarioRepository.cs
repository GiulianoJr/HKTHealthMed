using Dapper;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace PROJ_HealthMed.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<int> AddUsuario(Usuario usuario)
        {
            var sql = "INSERT INTO Usuario (Nome, CPF, CRM, Email, Senha, TipoUsuario, DataInicio, DataFim) VALUES (@Nome, @CPF, @CRM, @Email, @Senha, @TipoUsuario,@DataInicio, @DataFim); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, usuario);
        }

        public async Task<int> UpdateUsuario(Usuario usuario)
        {
            var sql = "UPDATE Usuario SET Nome = @Nome, CPF = @CPF, CRM = @CRM, Email = @Email, Senha = @Senha, TipoUsuario = @TipoUsuario, DataInicio = @DataInicio, DataFim = @DataFim WHERE IdUsuario = @IdUsuario";

            return _dbConnection.Execute(sql, usuario);
        }

        public async Task<Usuario> GetUsuarioById(int id)
        {
            var sql = "SELECT * FROM Usuario WHERE IdUsuario = @Id";

            return _dbConnection.Query<Usuario>(sql, new { Id = id }).SingleOrDefault();
        }

        public async Task<Usuario> GetUsuarioByEmailSenha(string email, string senha)
        {
            var sql = "SELECT * FROM Usuario WHERE Email = @Email AND Senha = @Senha";
            return _dbConnection.Query<Usuario>(sql, new { Email = email, Senha = senha}).SingleOrDefault();
        }
        public async Task<IEnumerable<Usuario>> GetMedicos()
        {
            var sql = "SELECT * FROM Usuario WHERE CRM <> ''";

            return _dbConnection.Query<Usuario>(sql);
        }
    }
}