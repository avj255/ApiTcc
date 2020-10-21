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
        public async Task<ActionResult<Pratos_DiaSemana>> PostPratos_DiaSemana(Pratos_DiaSemana pratos_DiaSemana)
        {
            _context.Pratos_DiaSemana.Add(pratos_DiaSemana);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPratos_DiaSemana", new { id = pratos_DiaSemana.idpratodia }, pratos_DiaSemana);
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
