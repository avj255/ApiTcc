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
    }
}
