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
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;

    public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config, IMapper mapper, ITokenService tokenService)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._config = (IConfigurationRoot)config;
        this._mapper = mapper;
        this._tokenService = tokenService;
    }

    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public ActionResult Get(){
        var identity = HttpContext.User.Identity;
  


        return Ok(identity);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] UserDTO model)
    {
        var user = new IdentityUser
        {
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

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

            TokenDTO tokenString = _tokenService.GerarToken(_config["Jwt:Key"],
                                                        _config["TokenConfigurantion:Issuer"],
                                                        _config["TokenConfigurantion:Audience"],
                                                        user);

            return Ok(new { token = tokenString });
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Login Invalido...");
            return BadRequest(ModelState);
        }
    }
}
