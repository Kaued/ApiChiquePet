using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiCatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Service;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.JsonWebTokens;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]

[ApiController]
public class AdminController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;

    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;   

    public AdminController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration config, IMapper mapper, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._config = (IConfigurationRoot)config;
        this._mapper = mapper;
        this._tokenService = tokenService;
        this._roleManager = roleManager;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult Get(){
        var identity = HttpContext.User.Identity;

        return Ok(identity);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterDTO model)
    {
        var user = new UserModel
        {
            UserName = model.Name,
            BirthDate = model.BirthDate,
            PhoneNumber = model.Phone,
            PhoneNumberConfirmed = true,
            Email = model.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, "Admin");

        await _signInManager.SignInAsync(user, false);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody] LoginDTO model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email,
        model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            UserModel user = _mapper.Map<UserModel>(model);

            IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model.Email));

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
}
