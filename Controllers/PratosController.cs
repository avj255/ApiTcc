using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTcc.Data;
using ApiTcc.Entidades;

namespace ApiTcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PratosController : ControllerBase
    {
        private readonly ApiTccContext _context;

        public PratosController(ApiTccContext context)
        {
            _context = context;
        }

        // GET: api/Pratos
        [HttpGet]
        public JsonResult GetPratos()
        {
            var pratos = _context.Pratos;
            foreach (Pratos prato in pratos)
            {
                prato.Ingredientes = GetIngredientes(prato);
            }

            return new JsonResult(pratos);
        }

        // GET: api/Pratos/ListaPratos
        [HttpGet]
        [Route("ListaPratos")]
        public JsonResult ListaPratos()
        {
            var pratos = _context.Pratos.Select(prato => new Pratos {pratoID = prato.pratoID,nome = prato.nome});
            return new JsonResult(pratos);
        }

        // GET: api/Pratos/5
        [HttpGet("{id}")]
        public JsonResult GetPratos(int id)
        {
            var pratos = _context.Pratos.Where(p => p.pratoID == id).ToList();

            if (pratos != null && pratos.Count > 0)
                pratos[0].Ingredientes = GetIngredientes(pratos[0]);

            return new JsonResult(pratos);
        }

        // POST: api/Pratos
        [HttpPost]
        public async Task<JsonResult> PostPratos(Pratos pratos)
        {
            var _prato = _context.Pratos.FirstOrDefault(e => e.nome == pratos.nome);

            if (_prato != null)
            {
                _prato.nome = pratos.nome;
                _prato.valor = pratos.valor;
                _prato.modopreparo = pratos.modopreparo;

                if (pratos.video != null)
                  _prato.video = pratos.video;

                if (pratos.foto != null)
                    _prato.foto = pratos.foto;
            } else
            {
                _context.Pratos.Add(pratos);
            }

            await _context.SaveChangesAsync();

            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        // DELETE: api/Pratos/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeletePratos(int id)
        {
            var pratos = await _context.Pratos.FindAsync(id);
            if (pratos == null)
            {
                return new JsonResult(new Resposta(2, "Prato não encontrado"));
            }

            _context.Pratos.Remove(pratos);
            await _context.SaveChangesAsync();

            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        private bool PratosExists(int id)
        {
            return _context.Pratos.Any(e => e.pratoID == id);
        }

        private List<Ingredientes> GetIngredientes(Pratos prato)
        {
            List<Ingredientes> ingredientes = new List<Ingredientes>();
            if (prato.pratos_Ingredientes != null)
            {
                foreach (Pratos_Ingredientes pi in prato.pratos_Ingredientes)
                {
                    ingredientes.Add(_context.Ingredientes.Find(pi.ingredienteID));
                }
            }
            return ingredientes;
        }
    }
}
