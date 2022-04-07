using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TesteTrivium.Data;
using TesteTrivium.Entities;

namespace TesteTrivium.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ILogger<ProdutoController> _logger;
        private readonly ApplicationContext _applicationContext;
        public ProdutoController(ILogger<ProdutoController> logger,
            ApplicationContext applicationContext)
        {
            _logger = logger;
            _applicationContext = applicationContext;
        }

        [HttpGet(Name = "GetProdutos")]
        [ProducesResponseType(201, Type = typeof(Produto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAll()
        {
            var listProdutos = await _applicationContext.Produtos.ToListAsync();
            return Ok(listProdutos);
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(Produto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Insert(Produto produto)
        {
            _applicationContext.Produtos.Add(produto);
            await _applicationContext.SaveChangesAsync();
            return Ok(produto.Id);
        }

        [HttpPut]
        [ProducesResponseType(201, Type = typeof(Produto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Update(int id, Produto produto)
        {
            var produtos = await _applicationContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produtos == null)
            {
                return BadRequest("Produto não encontrado");
            }
            _applicationContext.Entry(produtos).CurrentValues.SetValues(produto);
            await _applicationContext.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete]
        [ProducesResponseType(201, Type = typeof(Produto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Delete(int id)
        {
            var produto = await _applicationContext.Produtos.FirstOrDefaultAsync(x => x.Id == id);
            if (produto != null)
            {
                _applicationContext.Produtos.Remove(produto);
                await _applicationContext.SaveChangesAsync();
                return Ok();
            }
            return BadRequest("Produto não encontrado");
        }
    }
}
