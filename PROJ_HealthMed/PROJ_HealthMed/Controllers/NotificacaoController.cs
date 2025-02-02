﻿using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using PROJ_HealthMed.Models;
using PROJ_HealthMed.Interfaces;

namespace PROJ_HealthMed.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacaoController : ControllerBase
    {
        private readonly INotificacaoRepository _notificacaoRepository;

        public NotificacaoController(INotificacaoRepository notificacaoRepository)
        {
            _notificacaoRepository = notificacaoRepository;
        }

        [HttpPost("salvar")]
        public async Task<IActionResult> SaveNotificacao([FromBody] Notificacao notificacao)
        {
            var id = await _notificacaoRepository.AddNotificacao(notificacao);
            return CreatedAtAction(nameof(GetNotificacaoById), new { id }, notificacao);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notificacao>> GetNotificacaoById(int id)
        {
            var notificacao = await _notificacaoRepository.GetNotificacaoById(id);

            if (notificacao == null)
            {
                return NotFound();
            }

            return notificacao;
        }

        [HttpPost("enviar/{id}")]
        public async Task<IActionResult> EnviarNotificacao(int id)
        {
            var notificacao = await _notificacaoRepository.GetNotificacaoById(id);

            if (notificacao == null)
            {
                return NotFound();
            }

            var emailEnviado = EnviarEmail(notificacao);
            if (!emailEnviado)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, "Falha ao enviar e-mail.");
            }

            return Ok("E-mail enviado com sucesso.");
        }
        private bool EnviarEmail(Notificacao notificacao)
        {
            try
            {
                var fromAddress = new MailAddress("giulianojr@outlook.com", "Giuliano Reis Junior");
                var toAddress = new MailAddress("giulianojr@gmail.com", "Destinatário");
                const string fromPassword = "Fusion@211296";
                const string subject = "Notificação";
                string body = $"Status: {notificacao.StatusEnvio}\nData de Envio: {notificacao.DataEnvio}\nHora de Envio: {notificacao.HoraEnvio}";

                var smtp = new SmtpClient
                {
                    Host = "smtp.office365.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };

                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
