using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTrivium.Data;
using TesteTrivium.Entities;

namespace TesteTrivium.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly ILogger<ClienteController> _logger;
        private readonly ApplicationContext _applicationContext;
        public ClienteController(ILogger<ClienteController> logger,
            ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        [HttpGet(Name = "GetCliente")]
        [ProducesResponseType(201, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var listProdutos = await _applicationContext.Cliente.ToListAsync();
            return Ok(listProdutos);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Insert(Cliente cliente)
        {
            _applicationContext.Cliente.Add(cliente);
            await _applicationContext.SaveChangesAsync();
            return Ok(cliente.Id);
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, Cliente cliente)
        {
            var client = await _applicationContext.Cliente.FirstOrDefaultAsync(x => x.Id == id);
            if (client == null)
            {
                return BadRequest("Cliente não encontrado");
            }
            _applicationContext.Entry(client).CurrentValues.SetValues(cliente);
            await _applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(201, Type = typeof(Cliente))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _applicationContext.Cliente.FirstOrDefaultAsync(x => x.Id == id);
            if (cliente != null)
            {
                _applicationContext.Cliente.Remove(cliente);
                await _applicationContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Cliente não encontrado");
        }
    }
}
