using ApiCatalogo.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class AutorizaController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public AutorizaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        this._userManager= userManager;
        this._signInManager=signInManager;
    }

    [HttpGet]
    public ActionResult<string> Get(){
        return "AutorizaController ::   Acessado em :" + DateTime.Now.ToLongTimeString(); 
    }
    

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody]UserDTO model){
        var user = new IdentityUser{
            UserName = model.Email,
            Email = model.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded){
            return BadRequest(result.Errors);
        }

        await _signInManager.SignInAsync(user, false);
        return Ok();
    }

    [HttpPost("login")]
    public async Task<ActionResult> LoginUser([FromBody]UserDTO model){
        var result = await _signInManager.PasswordSignInAsync(model.Email,
        model.Password, isPersistent: false, lockoutOnFailure: false);

        if(result.Succeeded){
            return Ok();
        }else{
            ModelState.AddModelError(string.Empty, "Login Invalido...");
            return BadRequest(ModelState); 
        }
    }
    
}
