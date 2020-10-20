using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using ApiTcc.Data;
using ApiTcc.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiTcc.Controllers
{
    public class Pratos_IngredientesController : Controller
    {
        private readonly ApiTccContext _context;

        public Pratos_IngredientesController(ApiTccContext context)
        {
            _context = context;
        }

        // GET: api/Pratos_Ingredientes
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Pratos_Ingredientes>>> GetPratos_Ingredientes()
        {
            return await _context.Pratos_Ingredientes.ToListAsync();
        }

        // POST: api/Pratos_Ingredientes
        [HttpPost]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<Pratos_Ingredientes>>> PostPratos_Ingredientes(List<Pratos_Ingredientes> pratos_Ingredientes)
        {
            foreach (Pratos_Ingredientes ingrediente_prato in pratos_Ingredientes)
            {
                var pds = _context.Pratos_Ingredientes.Where(p => p.pratoID == ingrediente_prato.pratoID);

                foreach (Pratos_Ingredientes pd in pds)
                {
                    _context.Remove(pd);
                }

                _context.Pratos_Ingredientes.Add(ingrediente_prato);
            }
         
            await _context.SaveChangesAsync();
            return new JsonResult(new Resposta(1, "Sucesso"));
        }

}
}
