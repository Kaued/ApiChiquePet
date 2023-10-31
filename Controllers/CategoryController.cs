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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [EnableCors("Admin")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
        public ActionResult<IEnumerable<CategoryDTO>> Get([FromQuery] CategoryParameters categoryParameters)
        {
            var category = PageList<Category>.ToPageList(_context.Categories.AsNoTracking().OrderBy(on => on.Name), categoryParameters.PageNumber, categoryParameters.PageSize);

            if (category is null)
            {
                return NoContent();
            }
            var metadata = new
            {
                category.TotalCount,
                category.PageSize,
                category.TotalPages,
                category.HasNext,
                category.HasPrevious
            };

            var pagination = _mapper.Map<PaginationDTO>(category);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

            return _mapper.Map<List<CategoryDTO>>(category);
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoryProdutos()
        {
            var category = _context.Categories.AsNoTracking().Include(p => p.Products).ToList();
            if (category is null)
            {
                return NoContent();
            }

            return _mapper.Map<List<CategoryDTO>>(category);
        }
        [HttpGet("{id:int}", Name = "ObeterCategory")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.CategoryId == id);

            if (category is null)
            {
                return NoContent();
            }

            return _mapper.Map<CategoryDTO>(category);
        }

        [HttpPost]
        public ActionResult<CategoryDTO> Post(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            if (category is null)
            {
                return BadRequest();
            }

            var existCategory = _context.Categories.Where((cat) => cat.Name == category.Name).FirstOrDefault();

            if (existCategory is not null)
            {
                ModelState.AddModelError("name", "Nome da categoria já existe!");
                return BadRequest(ModelState);
            }

            _context.Categories.Add(category);
            _context.SaveChanges();

            var showCategory = _mapper.Map<CategoryDTO>(category);
            return new CreatedAtRouteResult("ObeterCategory", new { id = category.CategoryId }, showCategory);
        }

        [HttpPut("{id:int}")]

        public ActionResult<CategoryDTO> Put(int id, CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.CategoryId)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDTO);

            _context.Categories.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();

            var showCategory = _mapper.Map<CategoryDTO>(category);

            return Ok(showCategory);
        }

        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (category is null)
            {
                return BadRequest();
            }

            _context.Categories.Remove(category);
            _context.SaveChanges();

            var showCategory = _mapper.Map<CategoryDTO>(category);

            return Ok(showCategory);
        }
    }
}
