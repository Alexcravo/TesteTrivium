using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTrivium.Data;
using TesteTrivium.Entities;

namespace TesteTrivium.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompraController : ControllerBase
    {
        private readonly ILogger<CompraController> _logger;
        private readonly ApplicationContext _applicationContext;
        public CompraController(ILogger<CompraController> logger,
            ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        [HttpGet(Name = "GetCompra")]
        [ProducesResponseType(201, Type = typeof(Compra))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var listProdutos = await _applicationContext.Compra.ToListAsync();
            return Ok(listProdutos);
        }

        [HttpGet("{idcliente}")]
        [ProducesResponseType(201, Type = typeof(Compra))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetByClienteId(int idcliente)
        {
            var compraCliente = await _applicationContext.Compra
                .Include(x => x.Cliente)
                .FirstOrDefaultAsync(x => x.IdCliente == idcliente);
            if (compraCliente == null)
            {
                return NotFound("Compra não encontrado");
            }
            return Ok(compraCliente);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Compra))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Insert(Compra compra)
        {
            _applicationContext.Compra.Add(compra);
            await _applicationContext.SaveChangesAsync();
            return Ok(compra.Id);
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(Compra))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, Compra compra)
        {
            var compras = await _applicationContext.Compra.FirstOrDefaultAsync(x => x.Id == id);
            if (compras == null)
            {
                return NotFound("Compra não encontrado");
            }
            _applicationContext.Entry(compras).CurrentValues.SetValues(compra);
            await _applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(201, Type = typeof(Compra))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var compra = await _applicationContext.Compra.FirstOrDefaultAsync(x => x.Id == id);
            if (compra != null)
            {
                _applicationContext.Compra.Remove(compra);
                await _applicationContext.SaveChangesAsync();
                return Ok();
            }
            return NotFound("Compra não encontrado");
        }
    }
}
