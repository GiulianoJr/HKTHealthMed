using PROJ_HealthMed.Controllers;
using PROJ_HealthMed.Interfaces;
using PROJ_HealthMed.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Data.SqlClient;
using System.Data;

namespace PROJ_HealthMed
{
    public class Program
    {
        public static void Main(string[] args) 
        {
            var builder = WebApplication.CreateBuilder(args);

<<<<<<< HEAD
            // Configura��o de autentica��o JWT ppln
=======
            // Configuração de autenticação JWT ppln
>>>>>>> a367cc19cbfb7fd95214c6605470aa49d59b4540
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                };
            });

<<<<<<< HEAD
            // Configura��o da conex�o com o banco de dados
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var stringConexao = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            // Adicionar servi�os ao cont�iner
=======
            // Configuração da conexão com o banco de dados
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var stringConexao = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            // Adicionar serviços ao contêiner
>>>>>>> a367cc19cbfb7fd95214c6605470aa49d59b4540
            builder.Services.AddSingleton<IDbConnection>((conexao) => new SqlConnection(stringConexao));
            builder.Services.AddSingleton<IPaciente, PacienteRepository>();
            builder.Services.AddSingleton<IMedico, MedicoRepository>();
            builder.Services.AddSingleton<IAgenda, AgendaRepository>();
            builder.Services.AddSingleton<IAgendamento, AgendamentoRepository>();
            builder.Services.AddSingleton<TokenService>();
            builder.Services.AddControllers();

<<<<<<< HEAD
            // Configura��o do Swagger
=======
            // Configuração do Swagger
>>>>>>> a367cc19cbfb7fd95214c6605470aa49d59b4540
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

<<<<<<< HEAD
            // Configura��o do pipeline de requisi��es HTTP
=======
            // Configuração do pipeline de requisições HTTP
>>>>>>> a367cc19cbfb7fd95214c6605470aa49d59b4540
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
