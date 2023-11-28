using ApiChikPet.DTOs;
using ApiChikPet.Pagination;
using ApiChikPet.Context;
using ApiChikPet.Models;
using ApiChikPet.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiChikPet.Controllers
{
    [Route("[controller]")]
    [EnableCors("Admin")]
    [AllowAnonymous]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        private readonly ISaveFile _saveFile;

        public CategoryController(AppDbContext context, IMapper mapper, ISaveFile saveFile)
        {
            _context = context;
            _mapper = mapper;
            _saveFile = saveFile;
        }

        [HttpGet]
        [AllowAnonymous]   
        public ActionResult<IEnumerable<CategoryDTO>> Get([FromQuery] CategoryParameters categoryParameters)
        {
            var category = PageList<Category>.ToPageList(_context.Categories.AsNoTracking().OrderBy(on => on.Name), categoryParameters.PageNumber, categoryParameters.PageSize);

            if (category is null)
            {
                return NoContent();
            }
            
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

            var category = _context.Categories.AsNoTracking().Where((cat) => cat.Name!.ToLower().Contains(search.Trim().ToLower())).ToList();

            return Ok(_mapper.Map<List<ListCategoryDTO>>(category));
        }
        [HttpGet("{name}/products")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<CategoryDTO>> GetCategoryProducts([FromQuery] CategoryParameters categoryParameters, string name)
        {
            var product = PageList<Product>.ToPageList(_context.Products.AsNoTracking().Include((c)=>c.Category).Where((c) => c.Category.Name == name).Include((p)=>p.imageUrl).OrderBy((c)=>c.Name), categoryParameters.PageNumber, categoryParameters.PageSize);

            if (product is null)
            {
                return NoContent();
            }

            var pagination = _mapper.Map<PaginationDTO>(product);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

            return Ok(_mapper.Map<List<ListProductDTO>>(product));
        }
        [HttpGet("{id:int}", Name = "ObeterCategory")]
        public ActionResult<CategoryDTO> Get(int id)
        {
            var category = _context.Categories.AsNoTracking().FirstOrDefault(c => c.CategoryId == id);

            if (category is null)
            {
                return NoContent();
            }

            return Ok(_mapper.Map<ListCategoryDTO>(category));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
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

                var imageUrl = await _saveFile.SaveImage(categoryDTO.File);

                category.ImageUrl = imageUrl;
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
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
        public async Task<ActionResult<CategoryDTO>> Put(int id, [FromForm] CategoryDTO categoryDTO)
        {
            var category = _context.Categories.Where((c) => c.CategoryId == id).FirstOrDefault();

            if (category is null)
            {
                return BadRequest();
            }
            try
            {
                var imageUrl = await _saveFile.SaveImage(categoryDTO.File);
                _saveFile.RemoveFile(category.ImageUrl!);

                category.ImageUrl = imageUrl;
                category.Name = categoryDTO.Name;

                _context.Categories.Entry(category).State = EntityState.Modified;
                _context.SaveChanges();

                var showCategory = _mapper.Map<ListCategoryDTO>(category);
                return Ok(showCategory);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }


        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
        public ActionResult<CategoryDTO> Delete(int id)
        {
            var category = _context.Categories.FirstOrDefault(category => category.CategoryId == id);
            if (category is null)
            {
                return BadRequest();
            }

            if (category.ImageUrl != null)
            {
                try
                {
                    _saveFile.RemoveFile(category.ImageUrl!);
                }
                catch (Exception e)
                {
                    return BadRequest(e);
                }
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();

            var showCategory = _mapper.Map<CategoryDTO>(category);

            return Ok(showCategory);
        }
    }
}
