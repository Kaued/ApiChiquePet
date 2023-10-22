using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiCatalogo.DTOs;
using ApiCatalogo.Pagination;
using APICatalogo.Models;
using APICatalogo.Service;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[EnableCors("Admin")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller")]
public class AdminController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<UserModel> _password;
    private readonly IList<string> rolesAccepts = new List<string>(){
        "Seller",
        "Super Admin"
    };

    public AdminController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration config, IMapper mapper, ITokenService tokenService, IPasswordHasher<UserModel> passwordHasher)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._config = (IConfigurationRoot)config;
        this._mapper = mapper;
        this._tokenService = tokenService;
        this._password = passwordHasher;
    }

    [HttpGet]
    [EnableCors("Admin")]
    [Authorize(AuthenticationSchemes = "Bearer ", Roles = "Super Admin")]
    public async Task<ActionResult> GetAllAdmins([FromQuery] UsersParameters usersParameters)
    {
        var users = await PageList<UserModel>.ToPageListAsync(_userManager.Users.OrderBy((on) => on.UserName),
                                                                usersParameters.PageNumber,
                                                                usersParameters.PageSize);
        var pagination = _mapper.Map<PaginationDTO>(users);
        var usersDTO = _mapper.Map<List<RegisterDTO>>(users);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
        return Ok(usersDTO);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult> GetUser(string email)
    {

        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            var user = await _userManager.FindByEmailAsync(email);
            if (emailToken is not null && email == emailToken)
            {
                return (user is not null) ? Ok(user) : Unauthorized();
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(emailToken));
                var checkRoles = roles.Contains("Super Admin");

                if (checkRoles)
                {
                    ModelState.AddModelError("email", "Usuario não encontrado");
                    return (user is not null) ? Ok(roles) : BadRequest(ModelState);
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

    [HttpGet("roles")]
    public async Task<ActionResult> GetAllRolesUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is not null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            string email = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault().Value;
            if (email is not null)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(roles);
            }
            else
            {
                return BadRequest();
            }
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPost("register")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDTO model)
    {
        UserModel user = _mapper.Map<UserModel>(model);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, "Seller");

        await _signInManager.SignInAsync(user, false);

        return Ok(user.Email);
    }

    [HttpPut("{email}")]
    public async Task<ActionResult> UpdateUser([FromBody] UpdateAdminDTO model, string email)
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;
        if (identity is null)
        {
            return BadRequest();
        }

        IEnumerable<Claim> claims = identity.Claims;
        string? emailToken = claims.Where(x => x.Type == ClaimTypes.Name).FirstOrDefault()?.Value;
        if (emailToken is null)
            return BadRequest("Esqeuceu o email amigo");

        if (email != emailToken)
        {
            var user = await _userManager.FindByEmailAsync(emailToken);

            var roles = await _userManager.GetRolesAsync(user);
            var isNotSuperAdmin = roles.FirstOrDefault("Super Admin") is null;
            if (isNotSuperAdmin)
            {
                return Unauthorized();
            }

        }

        var updateResult = await Update(model, email);

        return updateResult is not null ? Ok(updateResult.Email) : BadRequest();


    }

    [HttpDelete("{email}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin")]
    public async Task<ActionResult> DeleteUser(string email)
    {
        UserModel? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            ModelState.AddModelError("email", "Usuario não encontrado");
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

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult> LoginUser([FromBody] LoginDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user,
            model.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model.Email));

                if (rolesAccepts.Any(x => userRoles.Any(y => x == y)))
                {

                    TokenDTO token = _tokenService.GerarToken(_config["Jwt:Key"],
                                                                _config["TokenConfigurantion:Issuer"],
                                                                _config["TokenConfigurantion:Audience"],
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

    private async Task<UserModel?> Update(UpdateAdminDTO model, string email)
    {

        UserModel userToChange = await _userManager.FindByEmailAsync(email);
        _mapper.Map<UpdateAdminDTO, UserModel>(model, userToChange);

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
