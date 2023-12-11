using System.Security.Claims;
using ApiChikPet.DTOs;
using ApiChikPet.DTOs.Users;
using ApiChikPet.Pagination;
using ApiChikPet.Context;
using ApiChikPet.Models;
using ApiChikPet.Service;
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
public class ClientController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<UserModel> _password;
    private readonly IList<string> rolesAccepts = new List<string>(){
        "Client",
        "Seller",
        "Super Admin"
    };

    public ClientController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration config, IMapper mapper, ITokenService tokenService, IPasswordHasher<UserModel> passwordHasher, AppDbContext context)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._config = (IConfigurationRoot)config;
        this._mapper = mapper;
        this._tokenService = tokenService;
        this._password = passwordHasher;
        this._context = context;
    }

    [HttpGet("{email}")]
    public async Task<ActionResult> GetUser(string email)
    {

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.Value;
            var user = await _userManager.FindByEmailAsync(email);

            var userDTO = _mapper.Map<ListUserDTO>(user);

            if (emailToken is not null && email == emailToken)
            {
                return (user is not null) ? Ok(userDTO) : Unauthorized();
            }
            else
            {
                var userIdentity = await _userManager.FindByEmailAsync(emailToken!);

                if (userIdentity is null)
                {
                    return BadRequest();
                }

                var roles = await _userManager.GetRolesAsync(userIdentity);
                var checkRoles = roles.Contains("Super Admin");

                if (checkRoles)
                {
                    ModelState.AddModelError("email", "Usuario não encontrado");
                    return (user is not null) ? Ok(userDTO) : BadRequest(ModelState);
                }
                else
                {
                    return Unauthorized();
                }
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpGet("orders")]
    public async Task<ActionResult> GetOrders([FromQuery] OrderParameters orderParameters)
    {

        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()!.Value;

            var user = await _userManager.FindByEmailAsync(emailToken);

            if (user is null)
            {
                return Unauthorized();
            }

            var orders = await PageList<Order>.ToPageListAsync(_context.Orders.Include((p) => p.User).Where((p) => p.User!.Email == user.Email).Include((p) => p.Address).Include((p)=>p.OrderProducts).ThenInclude((o)=>o.Product).OrderBy((p)=>p.CreateDate).AsNoTracking(), orderParameters.PageNumber, orderParameters.PageSize);

            var pagination = _mapper.Map<PaginationDTO>(orders);
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));

            return Ok(_mapper.Map<List<ListOrderDTO>>(orders));
        }

        return BadRequest();
    }

    [HttpPost()]
    [AllowAnonymous]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO model)
    {
        model.UserName = model.Email;
        UserModel user = _mapper.Map<UserModel>(model);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, "Client");

        await _signInManager.SignInAsync(user, false);

        return Ok(user.Email);
    }

    [HttpPut("{email}")]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateUserDTO model, string email)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return BadRequest();
        }

        IEnumerable<Claim> claims = identity.Claims;
        string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        if (emailToken is null)
            return BadRequest("E necessario informar o email");

        if (email != emailToken)
        {
            var user = await _userManager.FindByEmailAsync(emailToken);

            if (user is null)
            {
                return BadRequest();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var isNotSuperAdmin = roles.FirstOrDefault("Super Admin") is null;
            if (isNotSuperAdmin)
            {
                return Unauthorized();
            }

        }

        model.UserName = model.Email;
        var updateResult = await Update(model, email);

        return updateResult is not null ? Ok(updateResult.Email) : BadRequest();


    }

    [HttpDelete("{email}")]
    public async Task<ActionResult> DeleteUser(string email)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return BadRequest();
        }

        IEnumerable<Claim> claims = identity.Claims;
        string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        if (emailToken is null)
            return BadRequest("Usuário Invalido");

        if (email != emailToken)
        {
            return Unauthorized();
        }

        UserModel? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            ModelState.AddModelError("email", "Usuário não encontrado");
            return BadRequest(ModelState);
        }

        var result = await _userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        else
        {
            return Ok(user.Email);
        }
    }

    [HttpGet("address/{email}")]
    public async Task<ActionResult> GetAddress(string email)
    {

        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity is null)
        {
            return BadRequest();
        }

        IEnumerable<Claim> claims = identity.Claims;

        var userAddress = await _userManager.FindByEmailAsync(email);

        if (userAddress is null)
        {
            return NotFound();
        }

        string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        var address = await _context.Address.Include((a) => a.User).Where((a) => a.User!.Email == email).AsNoTracking().ToListAsync();

        if (emailToken is null && address is null)
        {
            return Unauthorized();
        }

        if (address is null)
        {
            return NotFound();
        }

        if (userAddress.Email != emailToken)
        {
            var user = await _userManager.FindByEmailAsync(emailToken!);

            var roles = await _userManager.GetRolesAsync(user!);
            var isNotSuperAdmin = roles.FirstOrDefault("Admin") is null;
            if (isNotSuperAdmin)
            {
                return Unauthorized();
            }

        }

        var addressDTO = _mapper.Map<List<ListAddressDTO>>(address);
        return Ok(addressDTO);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> LoginUser([FromBody] LoginUserDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user,
            model.Password!, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                IList<string> userRoles = await _userManager.GetRolesAsync(user);

                if (rolesAccepts.Any(x => userRoles.Any(y => x == y)))
                {

                    TokenDTO token = _tokenService.GerarToken(_config["Jwt:Key"]!,
                                                                _config["TokenConfigurantion:Issuer"]!,
                                                                _config["TokenConfigurantion:Audience"]!,
                                                                user,
                                                                userRoles);

                    return Ok(token);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Login Invalido...");
                    return BadRequest(ModelState);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Login Invalido...");
                return BadRequest(ModelState);
            }
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Login Invalido...");
            return BadRequest(ModelState);
        }
    }

    private async Task<UserModel?> Update(UpdateUserDTO model, string email)
    {

        UserModel? userToChange = await _userManager.FindByEmailAsync(email);

        if (userToChange is null)
        {
            return null;
        }

        _mapper.Map<UpdateUserDTO, UserModel>(model, userToChange!);

        if (model.Password is not null)
        {
            userToChange.PasswordHash = _password.HashPassword(userToChange, model.Password);
        }

        var result = await _userManager.UpdateAsync(userToChange);

        if (!result.Succeeded)
        {
            return null;
        }

        return userToChange;

    }
}
