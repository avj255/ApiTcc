using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTcc;
using ApiTcc.Data;
using System.Text.Json;
using ApiTcc.Retornos;
using System.Net.Mail;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Net;

namespace ApiTcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApiTccContext _context;

        public UsuariosController(ApiTccContext context)
        {
            _context = context;
        }


        [HttpPost]
        [Route("Login")]
        [Produces("application/json")]
        public JsonResult GetUsuarios(Usuarios usuario)
        {
           Usuarios usu = _context.Usuarios.FirstOrDefault(_usuario => _usuario.usuario == usuario.usuario && _usuario.senha == Criptografia.criptografar(usuario.senha));
           if (usu != null)
              return new JsonResult(new RespostaLogin(true, usu.nome, usu.administrador));
           else
              return new JsonResult(new RespostaLogin(false));
        }


        [HttpPost]
        [Route("Cadastro")]
        [Produces("application/json")]
        public async Task<JsonResult> PostUsuarios(Usuarios usuario)
        {
            if (_context.Usuarios.Any(e => e.usuario == usuario.usuario))
            {
                return new JsonResult(new Resposta(2, "Este usuário já existe"));
            }

            usuario.senha = Criptografia.criptografar(usuario.senha);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        [HttpPost]
        [Route("AlterarSenha")]
        [Produces("application/json")]
        public async Task<JsonResult> AlterarSenha(Usuarios usuario)
        {
            Usuarios _usuario = _context.Usuarios.FirstOrDefault(x => x.usuario == usuario.usuario);
            _usuario.senha = Criptografia.criptografar(usuario.senha);

            await _context.SaveChangesAsync();
            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        [HttpPost]
        [Route("RedefinirSenha")]
        [Produces("application/json")]
        public async Task<JsonResult> RedefinirSenha(Usuarios usuario)
        {

            Usuarios usuarioAux = _context.Usuarios.FirstOrDefault(x => x.usuario == usuario.usuario && x.email == usuario.email);

            if (usuarioAux == null)
            {
                return new JsonResult(new Resposta(2, "Usuário/Email não existem."));
            }
            else
            {
                String senhaAleatoria = GeraSenha();
                usuarioAux.senha = Criptografia.criptografar(senhaAleatoria);
                await _context.SaveChangesAsync();
                EnviarEmail(usuario.email, "Redefinição de Senha", "Sua Nova senha é: "+senhaAleatoria);

                return new JsonResult(new Resposta(1, "E-mail enviado com sucesso."));
            }
        }
        private string GeraSenha()
        {
            string guid = Guid.NewGuid().ToString().Replace("-", "");

            Random clsRan = new Random();
            Int32 tamanhoSenha = clsRan.Next(6, 18);

            string senha = "";
            for (Int32 i = 0; i <= tamanhoSenha; i++)
            {
                senha += guid.Substring(clsRan.Next(1, guid.Length), 1);
            }

            return senha;
        }

        public void EnviarEmail(string destinatario, string assunto, string corpo)
        {
            String emailOrigem = "";
            String senhaOrigem = "";
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = true;
            smtp.Timeout = 100000;
            smtp.Credentials = new NetworkCredential(
                emailOrigem,
                senhaOrigem
            );

            MailMessage message = new MailMessage();

            message.SubjectEncoding = Encoding.UTF8;
            message.Subject = assunto;
            message.Priority = 0;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = corpo;

            message.From = new MailAddress(emailOrigem);
            message.To.Add(new MailAddress(destinatario));

            smtp.Send(message);
        }
    }
}
