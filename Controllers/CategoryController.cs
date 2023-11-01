using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

        private readonly IWebHostEnvironment _enviroment;
        public CategoryController(AppDbContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            _enviroment = environment;
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

            return Ok(_mapper.Map<List<ListCategoryDTO>>(category));
        }

        [HttpGet("search/{search}")]
        public ActionResult<IEnumerable<CategoryDTO>> Search(string search)
        {

            if (search == "")
            {
                return BadRequest();
            }

            var category = _context.Categories.AsNoTracking().Where((cat) => cat.Name.ToLower().Contains(search.Trim().ToLower())).ToList();

            return Ok(_mapper.Map<List<ListCategoryDTO>>(category));
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
        public async Task<ActionResult<CategoryDTO>> Post([FromForm] CategoryDTO categoryDTO)
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

            try
            {

                var fileName = Path.GetFileName(categoryDTO.File.FileName);
                var path = Path.Combine(_enviroment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await categoryDTO.File.CopyToAsync(stream);
                }

                category.ImageUrl = "uploads/"+fileName;

                _context.Categories.Add(category);
                _context.SaveChanges();

                var showCategory = _mapper.Map<CategoryDTO>(category);
                return Ok(showCategory);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

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
