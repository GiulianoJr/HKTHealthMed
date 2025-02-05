using PROJ_HealthMed.Interfaces;
using PROJ_HealthMed.Models;
using System.Data;
using Dapper;
using System.Text.RegularExpressions;

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
            string CPFMedico = medico.CPF;
            string pattern = @"[-_,.!:;]";
            CPFMedico = Regex.Replace(CPFMedico, pattern, "");
            medico.CPF = CPFMedico;
            var sql = "INSERT INTO Medico (Nome, CPF, CRM, Email, Senha, Especialidade, ValorConsulta) VALUES (@Nome, @CPF, @CRM, @Email, @Senha, @Especialidade, @ValorConsulta); SELECT CAST(SCOPE_IDENTITY() as int);";
            return _dbConnection.Execute(sql, medico);
        }

        public async Task<IEnumerable<Agendamento>> GetAgendamentoPendente(int IdMedico)
        {
            var sql = "select * from Agendamento where MedicoDR = @IdMedico and StatusAgendamento = 'Pendente'";
            return _dbConnection.Query<Agendamento>(sql, new { IdMedico = IdMedico });
        }

        public async Task<int> UpdateMedico(Medico medico)
        {
            string CPFMedico = medico.CPF;
            string pattern = @"[-_,.!:;]";
            CPFMedico = Regex.Replace(CPFMedico, pattern, "");
            medico.CPF = CPFMedico;

            var sql = "UPDATE Medico SET Nome = @Nome, CPF = @CPF, CRM = @CRM, Email = @Email, Senha = @Senha, Especialidade = @Especialidade, ValorConsulta = @ValorConsulta WHERE IdUsuario = @IdMedico";

            return _dbConnection.Execute(sql, medico);
        }

        public async Task<int> AceiteAgendamento(int IdAgendamento, bool Aceite)
        {
            string StatusAgendamento = "Pendente";

            if (Aceite == true)
            {
                StatusAgendamento = "Confirmado";
            }
            else
            {
                StatusAgendamento = "Recusado";
            }

            var sql = "Update Agendamento set StatusAgendamento = @StatusAgendamento where IdAgendamento = @IdAgendamento";
            return _dbConnection.Execute(sql, new { StatusAgendamento = StatusAgendamento, IdAgendamento = IdAgendamento });
        }

        public async Task<IEnumerable<Medico>> GetMedicosBySpec(string Spec)
        {
            var sql = "SELECT * FROM Medico WHERE Especialidade = @Spec";

            return _dbConnection.Query<Medico>(sql, new { Spec = Spec });
        }
        public async Task<Medico> GetMedicoById(int id)
        {
            var sql = "SELECT * FROM Medico WHERE IdMedico = @Id";

            return _dbConnection.Query<Medico>(sql, new { Id = id }).SingleOrDefault();
        }

        public async Task<Medico> GetMedicoByCRMSenha(string crm, string senha)
        {
            var sql = "SELECT * FROM Medico WHERE CRM = @Crm AND Senha = @Senha";
            return _dbConnection.Query<Medico>(sql, new { Crm = crm, Senha = senha }).SingleOrDefault();
        }
        public async Task<IEnumerable<Medico>> GetMedicos()
        {
            var sql = "SELECT * FROM Medico";

            return _dbConnection.Query<Medico>(sql);
        }
    }
}
