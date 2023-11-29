using System.Security.Claims;
using ApiChikPet.DTOs;
using ApiChikPet.Pagination;
using ApiChikPet.Context;
using ApiChikPet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace ApiChikPet.Controllers;

[Route("[controller]")]
[EnableCors("Admin")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller,Client")]
public class OrderController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OrderController(UserManager<UserModel> userManager, IMapper mapper, AppDbContext context)
    {
        this._userManager = userManager; ;
        this._mapper = mapper;
        this._context = context;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller")]
    public async Task<ActionResult> Get([FromQuery] OrderParameters orderParameters, [FromQuery] int? statusId)
    {
        var contextOrder = _context.Orders.Include((p) => p.User).OrderBy((p) => p.CreateDate).AsNoTracking().Where((p) => p.StatusOrder != 8);

        if (statusId is not null)
        {

            if (statusId < 0 || statusId > 8)
            {
                return BadRequest();
            }

            contextOrder = _context.Orders.Include((p) => p.User).OrderBy((p) => p.CreateDate).AsNoTracking().Where((p) => p.StatusOrder == statusId);

        }

        var orders = await PageList<Order>.ToPageListAsync(contextOrder, orderParameters.PageNumber, orderParameters.PageSize);

        var pagination = _mapper.Map<PaginationDTO>(orders);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

        return Ok(_mapper.Map<List<ListOrderDTO>>(orders));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetOrder(int id)
    {

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return BadRequest();
        }

        IEnumerable<Claim> claims = identity.Claims;
        string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        if (emailToken is null)
            return BadRequest("Sem email no token");

        var user = await _userManager.FindByEmailAsync(emailToken);

        var order = await _context.Orders.Where((p) => p.OrderId == id).Include((p) => p.User).Include((p) => p.Address).Include((p) => p.OrderProducts).ThenInclude((o) => o.Product).ThenInclude((op)=>op!.imageUrl).FirstOrDefaultAsync();

        if (order is null)
        {
            return NotFound();
        }

        if (order.User!.Email != emailToken)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var isNotSuperAdmin = roles.FirstOrDefault("Super Admin") is null;
            var isNotSeller = roles.FirstOrDefault("Seller") is null;

            if (isNotSuperAdmin && isNotSeller)
            {
                return Unauthorized();
            }

        }

        return Ok(_mapper.Map<ListOrderDTO>(order));
    }


    [HttpGet("search/{search}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller")]
    public async Task<ActionResult> Get([FromQuery] int? statusId, string search)
    {
        var contextOrder = _context.Orders.Include((p) => p.User).OrderBy((p) => p.CreateDate).AsNoTracking().Where((p) => p.StatusOrder != 8);

        if (search == "")
        {
            return BadRequest();
        }

        if (statusId is not null)
        {

            if (statusId < 0 || statusId > 8)
            {
                return BadRequest();
            }

            contextOrder = _context.Orders.Include((p) => p.User).OrderBy((p) => p.CreateDate).AsNoTracking().Where((p) => p.StatusOrder == statusId);

        }

        var orders = contextOrder.Where((a) => a.User!.Email!.Contains(search));

        return Ok(_mapper.Map<List<ListOrderDTO>>(orders));
    }


    [HttpPost()]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller,Client")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterOrder model)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            var user = await _userManager.FindByEmailAsync(emailToken!);

            var address = await _context.Address.Where((p) => p.AddressId == model.AddressId).FirstOrDefaultAsync();

            if (address is null)
            {
                return BadRequest();
            }

            if (emailToken is null)
            {
                return Unauthorized();
            }

            if (model.Item.Count <= 0)
            {
                return BadRequest();
            }

            List<int> productId = new List<int>();

            foreach (var item in model.Item)
            {
                productId.Add(item.ProductId);
            }

            var product = await _context.Products.Where((p) => productId.Contains(p.ProductId)).ToListAsync();

            decimal totalPriceOrder = 0;

            foreach (var totalPrice in product)
            {
                totalPriceOrder = totalPriceOrder + totalPrice.Price * model.Item.Find((p) => p.ProductId == totalPrice.ProductId)!.Qtd;
            }

            var order = new Order()
            {
                AddressId = model.AddressId,
                CreateDate = DateTime.Now,
                IsOrder = model.IsOrder,
                StatusOrder = 0,
                TotalPrice = totalPriceOrder,
                UserId = user!.Id
            };

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            foreach (var item in model.Item)
            {
                var itemProduct = product.Find((p) => p.ProductId == item.ProductId);

                itemProduct!.Stock = itemProduct.Stock <= item.Qtd ? 0 : itemProduct.Stock - item.Qtd;

                var orderProduct = new OrderProduct()
                {
                    OrderId = order.OrderId,
                    ProductId = item.ProductId,
                    Qtd = item.Qtd
                };
                _context.Products.Entry(itemProduct).State = EntityState.Modified;
                _context.OrderProducts.Add(orderProduct);
            }

            _context.SaveChanges();

            return Ok(model);
        }

        return BadRequest();

    }

    [HttpPut("{id}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller")]
    public async Task<ActionResult> UpdateUser([FromBody] ChangeOrderDTO model, int id)
    {

        if (model.StatusId < 0 || model.StatusId > 8)
        {
            return BadRequest();
        }

        var order = await _context.Orders.Where((o) => o.OrderId == id).FirstOrDefaultAsync();

        if (order is not null)
        {

            order.StatusOrder = model.StatusId;
            _context.SaveChanges();

            return Ok();
        }

        return BadRequest();


    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var order = await _context.Orders.Where((o) => o.OrderId == id).FirstOrDefaultAsync();

        if (order is not null)
        {
            order.StatusOrder = 8;
            return Ok();
        }

        return BadRequest();
    }
}
