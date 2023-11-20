using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("[controller]")]
    [EnableCors("Admin")]
    [AllowAnonymous]
    [ApiController]
    [ServiceFilter(typeof(ApiLogginFilter))]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ProductController> _logger;
        private readonly IMapper _mapper;
         private readonly ISaveFile _saveFile;


        public ProductController(AppDbContext context, ILogger<ProductController> logger, IMapper mapper, ISaveFile saveFile)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _saveFile = saveFile;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ListProductDTO>> Get([FromQuery]ProductParameters productParameters){
            
            var products = PageList<Product>.ToPageList(_context.Products.AsNoTracking()
                .OrderBy(on => on.Name).Include((p)=>p.Category).Include((img)=>img.imageUrl), productParameters.PageNumber, productParameters.PageSize);

            _logger.LogInformation("<=============Get api/products==========>");

            var metadata = new{
                products.TotalCount,
                products.PageSize,
                products.TotalPages,
                products.HasNext,
                products.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return _mapper.Map<List<ListProductDTO>>(products);
        }

        [HttpGet("search/{search}")]
        public ActionResult<IEnumerable<ListProductDTO>> Search(string search)
        {

            if (search == "")
            {
                return BadRequest();
            }

            var products = _context.Products.AsNoTracking().Where((cat) => cat.Name!.ToLower().Contains(search.Trim().ToLower())).Include((p)=>p.imageUrl).Include((p)=>p.Category).ToList();

            return Ok(_mapper.Map<List<ListProductDTO>>(products));
        }

        [HttpGet("{id:int}", Name="ObeterProduct")]
        public ActionResult<ListProductDTO> Get(int id){
            var product = _context.Products.Where((p)=>p.ProductId==id).Include((p)=>p.Category).Include((img)=>img.imageUrl).AsNoTracking().FirstOrDefault();

            if(product == null){
                ModelState.AddModelError("name", "Produto não existe!");
                return NotFound(ModelState);
            }

            return _mapper.Map<ListProductDTO>(product);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
        public async Task<ActionResult<ListProductDTO>> Post([FromForm] ProductDTO productDTO)
        {
            var product = _mapper.Map<Product>(productDTO); 
            if(product is null){
                ModelState.AddModelError("name", "Erro ao cadastrar o produto!");
                return BadRequest(ModelState); 
            }
            var search = _context.Products.Where((p)=>p.Name==product.Name).FirstOrDefault();
            if(search is not null){
                ModelState.AddModelError("name", "Nome do produto já existe!");
                return BadRequest(ModelState);
            }

            

             try
            {
                var data = DateTime.Now;
                var day = data.Day;
                var month = data.Month;
                var year = data.Year;
                var hour = data.TimeOfDay.Hours;
                var minute = data.TimeOfDay.Minutes;
                var second = data.TimeOfDay.Seconds;

                var formatData = new DateTime(year,month,day,hour,minute,second);                                

                product.DateRegister = formatData;

                // return Ok(productDTO.Cover);
                _context.Products.Add(product);
                _context.SaveChanges();


                var coverUrl = await _saveFile.SaveImage(productDTO.Cover);

                var cover = new ImageUrl(){
                    Path=coverUrl,
                    ProductId=product.ProductId,
                    Type="cover"
                };

                _context.ImageUrl.Add(cover);

                
                foreach (var file in productDTO.Gallery)
                {
                    var galleryUrl = await _saveFile.SaveImage(file);

                    var gallery = new ImageUrl(){
                        Path=galleryUrl,
                        ProductId=product.ProductId,
                        Type="gallery"
                    };

                    _context.ImageUrl.Add(gallery);
                }

                _context.SaveChanges();

                var showProduct = _mapper.Map<ListProductDTO>(product);
                return Ok(showProduct);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }            
        }

        [HttpPut("{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
        public async Task<ActionResult<ListProductDTO>> Put(int id, [FromForm]ProductDTO productDTO){
            var product = _context.Products.Where((p) => p.ProductId == id).Include((img)=>img.imageUrl).FirstOrDefault();

            if(product is null){
                ModelState.AddModelError("name", "Produto não existe!");
                return BadRequest(ModelState);
            }

            try
            {

                var productCreate = _mapper.Map<Product>(productDTO);
                productCreate.ProductId=product.ProductId;
                
                product.Name = productCreate.Name;
                product.Description = productCreate.Description;
                product.Price = productCreate.Price;
                product.CategoryId = productCreate.CategoryId;
                product.Height = productCreate.Height;
                product.Stock = productCreate.Stock;
                product.Width = productCreate.Width;

                foreach(var img in product.imageUrl!){
                    _saveFile.RemoveFile(img.Path!);
                }
                product.imageUrl.Clear();

                var coverUrl = await _saveFile.SaveImage(productDTO.Cover);

                var cover = new ImageUrl(){
                    Path=coverUrl,
                    ProductId=product.ProductId,
                    Type="cover"
                };

                product.imageUrl.Add(cover);

                foreach (var file in productDTO.Gallery)
                {
                    var galleryUrl = await _saveFile.SaveImage(file);

                    var gallery = new ImageUrl(){
                        Path=galleryUrl,
                        ProductId=product.ProductId,
                        Type="gallery"
                    };

                    product.imageUrl.Add(gallery);
                }

                _context.Products.Update(product);
                
                _context.SaveChanges();
                 var showProduct = _mapper.Map<ProductDTO>(productCreate);
                return Ok(showProduct);
            }

            catch (Exception e)
            {
                return BadRequest(e);
            }  

        }

        [HttpDelete("{id:int}")]
        [Authorize(AuthenticationSchemes = "Bearer", Roles = "Seller")]
        public ActionResult<ListProductDTO> Delete(int id)
        {
            var products = _context.Products.Include((img)=>img.imageUrl).FirstOrDefault(p => p.ProductId == id);

            if (products is null)
            {
                ModelState.AddModelError("name", "Produto não existe!");
                return BadRequest(ModelState);
            }
            foreach(var img in products.imageUrl!){
                _saveFile.RemoveFile(img.Path!);
            }
            products.imageUrl.Clear();
            
            _context.Products.Remove(products);

            _context.SaveChanges();

            var showProduct = _mapper.Map<ProductDTO>(products);
            return Ok(showProduct);
        }
    }
}
