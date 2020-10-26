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
    public class Pratos_DiaSemanaController : ControllerBase
    {
        private readonly ApiTccContext _context;

        public Pratos_DiaSemanaController(ApiTccContext context)
        {
            _context = context;
        }

        // GET: api/Pratos_DiaSemana
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Pratos_DiaSemana>>> GetPratos_DiaSemana()
        {
            return await _context.Pratos_DiaSemana.ToListAsync();
        }

        [HttpGet("{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Pratos_DiaSemana>>> GetPratos_Dia(int id)
        {
            var pds = _context.Pratos_DiaSemana.Where(p => p.diasemana == id);
            foreach (Pratos_DiaSemana pd in pds)
            {
                pd.prato.Ingredientes = GetIngredientes(pd.prato);
                pd.prato.foto = Convert.ToBase64String(pd.prato.fotobin);
                pd.prato.fotobin = null;
            }

            return await pds.ToListAsync();
        }

        [HttpGet]
        [Route("PratoDiaSemImagem/{id}")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Pratos>>> GetPratos_DiaSemImagem(int id)
        {
            var pds = _context.Pratos_DiaSemana
                .Where(p => p.diasemana == id)
                .Select(s => new Pratos { pratoID = s.prato.pratoID, nome = s.prato.nome, valor = s.prato.valor })
                .OrderBy(o => o.nome);

            return await pds.ToListAsync();
        }

        [HttpGet]
        [Route("PratoDia")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Pratos_DiaSemana>>> GetPratos_Dia()
        {
            int diaSemana = (int)TimeZoneInfo.ConvertTime(DateTime.Now, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).DayOfWeek;

            if (diaSemana == 0)
                diaSemana = 7;

            return await _context.Pratos_DiaSemana.Where(p => p.diasemana == diaSemana).ToListAsync();
        }

        // POST: api/Pratos_DiaSemana
        [HttpPost]
        [Produces("application/json")]
        public async Task<JsonResult> PostPratos_DiaSemana(Pratos_DiaSemana pratos_DiaSemana)
        {
            var pds = _context.Pratos_DiaSemana.FirstOrDefault(p => p.diasemana == pratos_DiaSemana.diasemana && p.prato.pratoID == pratos_DiaSemana.prato.pratoID);
            
            if (pds == null)
            {
                pratos_DiaSemana.prato = _context.Pratos.FirstOrDefault(p => p.pratoID == pratos_DiaSemana.prato.pratoID);
                _context.Pratos_DiaSemana.Add(pratos_DiaSemana);

                await _context.SaveChangesAsync();
            }

            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        [HttpPost]
        [Route("DesvincularPratoDiaSemana")]
        [Produces("application/json")]
        public async Task<JsonResult> DesvincularPrato(Pratos_DiaSemana pratos_DiaSemana)
        {
            var pds = _context.Pratos_DiaSemana.FirstOrDefault(p => p.diasemana == pratos_DiaSemana.diasemana && p.prato.pratoID == pratos_DiaSemana.prato.pratoID);

            if (pds != null)
            {
                _context.Pratos_DiaSemana.Remove(pds);

                await _context.SaveChangesAsync();
            }

            return new JsonResult(new Resposta(1, "Sucesso"));
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
