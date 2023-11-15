using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ServiceFilter(typeof(ApiLogginFilter))]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProdutosController> _logger;
        private readonly IMapper _mapper;

        public ProdutosController(AppDbContext context, ILogger<ProdutosController> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProdutoDTO>> Get([FromQuery]ProdutosParameters produtosParameters){
            var produtos = PageList<Produto>.ToPageList(_context.Produtos.AsNoTracking()
                .OrderBy(on => on.Nome), produtosParameters.PageNumber, produtosParameters.PageSize);

            _logger.LogInformation("<=============Get api/produtos==========>");
            if(produtos is null){
                return  NotFound();
            }

            var metadata = new{
                produtos.TotalCount,
                produtos.PageSize,
                produtos.TotalPages,
                produtos.HasNext,
                produtos.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return _mapper.Map<List<ProdutoDTO>>(produtos);
        }

        [HttpGet("id:int", Name="ObeterProduto")]
        public ActionResult<ProdutoDTO> Get(int id){
            var produto = _context.Produtos.AsNoTracking().FirstOrDefault(p=>p.ProdutoId == id);

            if(produto == null){
                return NotFound();
            }

            return _mapper.Map<ProdutoDTO>(produto);
        }

        [HttpPost]
        public ActionResult Post(ProdutoDTO produtoDTO)
        {
            var produto = _mapper.Map<Produto>(produtoDTO); 
            if(produto is null){
                return BadRequest(); 
            } 

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            var showProduto = _mapper.Map<ProdutoDTO>(produto);
            return new CreatedAtRouteResult("ObeterProduto", 
                new {id= produto.ProdutoId}, showProduto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, ProdutoDTO produtoDTO){
            if(id != produtoDTO.ProdutoId){
                return BadRequest();
            }

            var produtos = _mapper.Map<Produto>(produtoDTO);
            _context.Produtos.Entry(produtos).State = EntityState.Modified;
            _context.SaveChanges();
            
            var showProduto = _mapper.Map<ProdutoDTO>(produtos);
            return Ok(showProduto);
        }
    
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id){
            var produtos = _context.Produtos.FirstOrDefault(p=>p.ProdutoId==id);

            if (produtos is null){
                return BadRequest();
            }

            _context.Produtos.Remove(produtos);
            _context.SaveChanges(); 

            var showProduto = _mapper.Map<ProdutoDTO>(produtos);
            return Ok(showProduto);
        }    
    }
}
