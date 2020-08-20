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

        // GET: api/Usuarios
        [HttpGet]
        [Produces("application/json")]
        public string GetUsuarios(Usuarios usuario)
        {
            try
            {
                Usuarios usu = _context.Usuarios.FirstOrDefault(_usuario => _usuario.usuario == usuario.usuario && _usuario.senha == Criptografia.criptografar(usuario.senha));
                if (usu != null)
                   return JsonSerializer.Serialize(new RespostaLogin(true, true, usu.nome, usu.administrador));
                else
                   return JsonSerializer.Serialize(new RespostaLogin(true, false));
            }
            catch (Exception e)
            {
                return JsonSerializer.Serialize(new RespostaLogin(false, false));
            }
        }

        // POST: api/Usuarios
        [HttpPost]
        [Produces("application/json")]
        public async Task<string> PostUsuarios(Usuarios usuario)
        {
            usuario.senha = Criptografia.criptografar(usuario.senha);
            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
                return JsonSerializer.Serialize(new Resposta(true));
            }
            catch
            {
                return JsonSerializer.Serialize(new Resposta(false));
            }
        }

        private bool UsuariosExists(int id)
        {
            return _context.Usuarios.Any(e => e.userID == id);
        }
    }
}
