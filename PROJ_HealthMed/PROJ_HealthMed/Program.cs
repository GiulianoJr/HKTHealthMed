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

            // Configuração de autenticação JWT
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

            // Configuração da conexão com o banco de dados
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var stringConexao = configuration.GetValue<string>("ConnectionStrings:DefaultConnection");

            // Adicionar serviços ao contêiner
            builder.Services.AddSingleton<IDbConnection>((conexao) => new SqlConnection(stringConexao));
            builder.Services.AddSingleton<IPaciente, PacienteRepository>();
            builder.Services.AddSingleton<IMedico, MedicoRepository>();
            builder.Services.AddSingleton<IAgenda, AgendaRepository>();
            builder.Services.AddSingleton<IAgendamento, AgendamentoRepository>();
            builder.Services.AddSingleton<TokenService>();
            builder.Services.AddControllers();

            // Configuração do Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configuração do pipeline de requisições HTTP
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
