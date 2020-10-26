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
    public class IngredientesController : ControllerBase
    {
        private readonly ApiTccContext _context;

        public IngredientesController(ApiTccContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetIngredientes()
        {
            var ingredientes = _context.Ingredientes.Select(i => new Ingredientes { ingredienteID = i.ingredienteID, nome = i.nome, calorias = i.calorias, peso = i.peso }).OrderBy(p => p.nome);

            return new JsonResult(ingredientes);
        }

        // POST: api/Ingredientes
        [HttpPost]
        public async Task<JsonResult> PostIngredientes(Ingredientes ingredientes)
        {
            var ingrediente = _context.Ingredientes.FirstOrDefault(e => e.ingredienteID == ingredientes.ingredienteID);

            if (ingrediente != null)
            {
                ingrediente.nome = ingredientes.nome;
                ingrediente.peso = ingredientes.peso;
                ingrediente.calorias = ingredientes.calorias;
            } else
            {
                _context.Ingredientes.Add(ingredientes);
            }

            await _context.SaveChangesAsync();

            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        // DELETE: api/Ingredientes/5
        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteIngredientes(int id)
        {
            var ingredientes = await _context.Ingredientes.FindAsync(id);
            if (ingredientes == null)
            {
                return new JsonResult(new Resposta(2, "Ingrediente não encontrado"));
            }

            _context.Ingredientes.Remove(ingredientes);
            await _context.SaveChangesAsync();

            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        private bool IngredientesExists(int id)
        {
            return _context.Ingredientes.Any(e => e.ingredienteID == id);
        }
    }
}
