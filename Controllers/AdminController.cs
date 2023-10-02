using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ApiCatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public AdminController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IConfiguration config, IMapper mapper)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._config=(IConfigurationRoot)config;
        this._mapper=mapper;
    }

    [HttpGet]
    public ActionResult<string> Get()
    {
        return "SellerLoginController ::   Acessado em :" + DateTime.Now.ToLongTimeString();
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
    public async Task<ActionResult> LoginUser([FromBody] UserDTO model)
    {
        var result = await _signInManager.PasswordSignInAsync(model.Email,
        model.Password, isPersistent: false, lockoutOnFailure: false);

        if (result.Succeeded)
        {   
            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.UniqueName, model.Email),
                new Claim(JwtRegisteredClaimNames.CHash)
            }
            var tokenString = TokenService.GerarToken(_config["Jwt:Key"],
                                                     _config["Jwt:Issuer"],
                                                     _config["Jwt:Audience"],
                                                     _mapper.Map<UserModel>(model));

            return Ok(new { tokern = tokenString });
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Login Invalido...");
            return BadRequest(ModelState);
        }
    }

}
