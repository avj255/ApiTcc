using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ApiTcc.Data;
using ApiTcc.Entidades;
using System.Security.Cryptography.X509Certificates;

namespace ApiTcc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly ApiTccContext _context;

        public PedidosController(ApiTccContext context)
        {
            _context = context;
        }

        // GET: api/Pedidos
        [HttpGet]
        public JsonResult GetPedidos()
        {
            var pedidos = _context.Pedidos;

            return new JsonResult(pedidos);
        }

        // GET: api/Pedidos/5
        [HttpGet("{id}")]
        public JsonResult GetPedidos(int id)
        {
            var pedidos = _context.Pedidos.Where(p => p.usuario == id).ToList();
            foreach (Pedidos pedido in pedidos)
            {
                pedido.pratoObj = _context.Pratos.First(p => p.pratoID == pedido.prato);
                pedido.pratoObj.foto = null;
                pedido.pratoObj.video = null;
            }

            return new JsonResult(pedidos);
        }

        // GET: api/Pedidos/PratosMaisPedido
        [HttpGet]
        [Route("PratosMaisPedido")]
        public JsonResult GetPratosMaisPedidos()
        {

            List<Pedidos> pedidos = _context.Pedidos.GroupBy(r => new { r.prato }).Select(g => new Pedidos { quantidade = g.Sum(c => c.quantidade), prato = g.Key.prato }).OrderByDescending(i => i.quantidade).Take(5).ToList();

            foreach (Pedidos pi in pedidos)
            {
                pi.pratoObj = _context.Pratos.Where(x => x.pratoID == pi.prato).Select(y => new Pratos { nome = y.nome }).FirstOrDefault();

            }

            return new JsonResult(pedidos);
        }

        // GET: api/Pedidos/Situacoes
        [HttpGet]
        [Route("Situacoes")]
        public JsonResult GetSituacoes()
        {
            return new JsonResult(_context.Pedidos.GroupBy(r => new { r.situacao }).Select(g => new Pedidos { quantidade = g.Count(), situacao = g.Key.situacao }).OrderByDescending(i => i.quantidade).ToList());
        }

        // POST: api/Pedidos
        [HttpPost]
        public async Task<JsonResult> PostPedidos(Pedidos pedidos)
        {
            _context.Pedidos.Add(pedidos);

            await _context.SaveChangesAsync();

            return new JsonResult(new Resposta(1, "Sucesso"));
        }


        // DELETE: api/Pedidos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pedidos>> DeletePedidos(int id)
        {
            var pedidos = await _context.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return new JsonResult(new Resposta(2, "Pedido não encontrado"));
            }

            _context.Pedidos.Remove(pedidos);
            await _context.SaveChangesAsync();

            return new JsonResult(new Resposta(1, "Sucesso"));
        }

        private bool PedidosExists(int id)
        {
            return _context.Pedidos.Any(e => e.pedidoID == id);
        }
    }
}
