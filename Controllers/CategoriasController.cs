using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using APICatalogo.Context;
using APICatalogo.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoriasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes ="Bearer", Roles = "Admin")]
        public ActionResult<IEnumerable<CategoriaDTO>> Get([FromQuery] CategoriaParameters categoriaParameters){
            var categorias =  PageList<Categoria>.ToPageList(_context.Categorias.AsNoTracking().OrderBy(on => on.Nome), categoriaParameters.PageNumber, categoriaParameters.PageSize);

            if(categorias is null){
                return NoContent();
            }
            var metadata = new {
                categorias.TotalCount,
                categorias.PageSize,
                categorias.TotalPages,
                categorias.HasNext,
                categorias.HasPrevious
            };
            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoriaDTO>> GetCategoriasProdutos(){
            var categorias = _context.Categorias.AsNoTracking().Include(p => p.Produtos).ToList();
            if(categorias is null){
                return NoContent();
            }

            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }
        [HttpGet("{id:int}", Name = "ObeterCategoria")]
        public ActionResult<CategoriaDTO> Get(int id){
            var categorias = _context.Categorias.AsNoTracking().FirstOrDefault(c => c.CategoriaId == id);

            if(categorias is null){
                return NoContent();
            }

            return _mapper.Map<CategoriaDTO>(categorias);
        }

        [HttpPost]
        public ActionResult<CategoriaDTO> Post(CategoriaDTO categoriaDTO){
            var categoria = _mapper.Map<Categoria>(categoriaDTO);
            if(categoria is null){
                return BadRequest();
            }	
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            
            var showCategoria = _mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("ObeterCategoria", new {id = categoria.CategoriaId}, showCategoria);
        }

        [HttpPut("{id:int}")]

        public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDTO){
            if(id != categoriaDTO.CategoriaId){
                return BadRequest();
            }

            var categoria = _mapper.Map<Categoria>(categoriaDTO);

            _context.Categorias.Entry(categoria).State = EntityState.Modified;
            _context.SaveChanges();

            var showCategoria = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(showCategoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoriaDTO> Delete(int id){
            var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.CategoriaId == id);
            if(categoria is null){
                return BadRequest();
            }

            _context.Categorias.Remove(categoria);
            _context.SaveChanges();

            var showCategoria = _mapper.Map<CategoriaDTO>(categoria);

            return Ok(showCategoria);
        }
    }
}
