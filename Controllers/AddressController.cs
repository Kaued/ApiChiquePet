using System.Security.Claims;
using ApiChikPet.Context;
using ApiChikPet.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiChikPet.Controllers;

[Route("[controller]")]
[EnableCors("Admin")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller,Client")]
public class AddressController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public AddressController(UserManager<UserModel> userManager, IMapper mapper, AppDbContext context)
    {
        this._userManager = userManager;;
        this._mapper = mapper;
        this._context = context;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUser(int id)
    {

        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity is null)
        {
            return BadRequest();
        }

        IEnumerable<Claim> claims = identity.Claims;
        string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        var address = await _context.Address.Include((a) => a.User).Where((a) => a.AddressId == id).FirstOrDefaultAsync();

        if (emailToken is null && address is null)
        {
            return Unauthorized();
        }

        if (address is null)
        {
            return NotFound();
        }

        if (address.User!.Email != emailToken)
        {
            var user = await _userManager.FindByEmailAsync(emailToken!);

            var roles = await _userManager.GetRolesAsync(user!);
            var isNotSuperAdmin = roles.FirstOrDefault("Admin") is null;
            if (isNotSuperAdmin)
            {
                return Unauthorized();
            }

        }

        var addressDTO = _mapper.Map<ListAddressDTO>(address);
        return Ok(addressDTO);
    }


    [HttpPost()]
    public async Task<ActionResult> RegisterUser([FromBody] AddressDTO model)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
            var user = await _userManager.FindByEmailAsync(emailToken!);

            if (emailToken is null)
            {
                return Unauthorized();
            }

            var address = _mapper.Map<Address>(model);

            address.UserId = user?.Id;

            _context.Address.Add(address);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        return BadRequest();

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser([FromBody] AddressDTO model, int id)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

            if (emailToken is null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(emailToken!);

            if (user is null)
            {
                return Unauthorized();
            }

            var editAddress = await _context.Address.Include((a) => a.User).Where((a) => a.AddressId == id).AsNoTracking().FirstOrDefaultAsync();

            if (editAddress is null)
            {
                return BadRequest();
            }

            if (user.Email != editAddress.User!.Email)
            {
                return Unauthorized();
            }

            var address = _mapper.Map<Address>(model);

            address.UserId = user?.Id;
            address.AddressId = id;

            _context.Address.Entry(address).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        return BadRequest();


    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(int id)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;

            if (emailToken is null)
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByEmailAsync(emailToken!);

            if (user is null)
            {
                return Unauthorized();
            }

            var removeAddress = await _context.Address.Include((a) => a.User).Where((a) => a.AddressId == id).AsNoTracking().FirstOrDefaultAsync();

            if (removeAddress is null)
            {
                return BadRequest();
            }

            if (user.Email != removeAddress.User!.Email)
            {
                return Unauthorized();
            }
            
            removeAddress.User = null;

            _context.Remove(removeAddress);

            await _context.SaveChangesAsync();

            var addressDTO = _mapper.Map<ListAddressDTO>(removeAddress);

            return Ok(removeAddress);
        }

        return BadRequest();
    }
}
