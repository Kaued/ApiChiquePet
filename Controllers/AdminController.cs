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
using System.Web;
using ApiChikPet.Services;

namespace ApiChikPet.Controllers;

[Route("[controller]")]
[EnableCors("Admin")]
[ApiController]
[Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller")]
public class AdminController : ControllerBase
{
    private readonly UserManager<UserModel> _userManager;
    private readonly SignInManager<UserModel> _signInManager;
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;
    private readonly IMapper _mapper;
    private readonly ITokenService _tokenService;
    private readonly IPasswordHasher<UserModel> _password;
    private readonly IEmailSender _emailSender;
    private readonly IList<string> rolesAccepts = new List<string>(){
        "Seller",
        "Super Admin"
    };

    public AdminController(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IConfiguration config, IMapper mapper, ITokenService tokenService, IPasswordHasher<UserModel> passwordHasher, AppDbContext context, IEmailSender emailSender)
    {
        this._userManager = userManager;
        this._signInManager = signInManager;
        this._config = (IConfigurationRoot)config;
        this._mapper = mapper;
        this._tokenService = tokenService;
        this._password = passwordHasher;
        this._context = context;
        this._emailSender = emailSender;
    }

    [HttpGet]
    [EnableCors("Admin")]
    [Authorize(AuthenticationSchemes = "Bearer ", Roles = "Super Admin")]
    public async Task<ActionResult> GetAllAdmins([FromQuery] UsersParameters usersParameters)
    {
        var users = await PageList<UserModel>.ToPageListAsync(_userManager.Users.OrderBy((on) => on.UserName)
                                                                .Join(
                                                                    _context.UserRoles,
                                                                    (u) => u.Id,
                                                                    (ur) => ur.UserId,
                                                                    (u, ur) => new
                                                                    {
                                                                        u,
                                                                        ur
                                                                    }
                                                                ).Join(
                                                                    _context.Roles,
                                                                    (ur) => ur.ur.RoleId,
                                                                    (r) => r.Id,
                                                                    (ur, r) => new
                                                                    {
                                                                        User = ur.u,
                                                                        Role = r
                                                                    }
                                                                ).Where((r) => r.Role.Name == "Seller")
                                                                .Select((u) => u.User as UserModel),
                                                                usersParameters.PageNumber,
                                                                usersParameters.PageSize);
        var pagination = _mapper.Map<PaginationDTO>(users);
        var usersDTO = _mapper.Map<List<ListUserDTO>>(users);
        Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(pagination));
        return Ok(usersDTO);
    }

    [HttpGet("search/{email}")]
    [EnableCors("Admin")]
    [Authorize(AuthenticationSchemes = "Bearer ", Roles = "Super Admin")]
    public async Task<ActionResult> Search(string email)
    {
        var users = await _userManager.Users
                        .Where((on) => on.Email.Contains(email.Trim()))
                        .OrderBy((on) => on.Email)
                        .Join(
                            _context.UserRoles,
                            (u) => u.Id,
                            (ur) => ur.UserId,
                            (u, ur) => new
                            {
                                u,
                                ur
                            }
                        ).Join(
                            _context.Roles,
                            (ur) => ur.ur.RoleId,
                            (r) => r.Id,
                            (ur, r) => new
                            {
                                User = ur.u,
                                Role = r
                            }
                        ).Where((r) => r.Role.Name == "Seller")
                        .Select((u) => u.User as UserModel)
                        .ToListAsync();

        var userDTO = _mapper.Map<List<ListUserDTO>>(users);
        return Ok(userDTO);
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

            var userDTO = _mapper.Map<ListUserDTO>(user);

            if (emailToken is not null && email == emailToken)
            {
                return (user is not null) ? Ok(userDTO) : Unauthorized();
            }
            else
            {
                var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(emailToken));
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

    [HttpPost()]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO model)
    {
        model.UserName = model.Email;
        UserModel user = _mapper.Map<UserModel>(model);

        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        await _userManager.AddToRoleAsync(user, "Seller");

        var userDb = await _userManager.FindByEmailAsync(user.Email!);
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(userDb!);

        var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]!);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["token"] = token;
        query["userid"] = userDb!.Id;
        uriBuilder.Query = query.ToString();
        var urlString = uriBuilder.ToString();

        var message = @"<p>Oi tudo bem? Estamos aqui para confirmar o seu cadastro no site da <b>ChikPet</b>. Seu cadastro em para o email " + user.Email + ". Clique no link abaixo para confirmar o seu cadastro</p><br/><a href='" + urlString + "' style='display: block; padding:40px; text-align: center; font-size: 28px; font-weigh: 700; text-decoration: none; color: black; background: #d1d1d1; border-radius: 10px; margin-left: auto; margin-right: auto;'>Clique aqui para confimar</a> ";

        var senderEmail = _config["ReturnPaths:SenderEmail"];
        await _emailSender.SendEmailAsync(senderEmail!, userDb.Email!, "Confirme o seu e-mail", message, "Confirme o seu email");

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
            return BadRequest("Sem email no token");

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

        model.UserName = model.Email;
        var updateResult = await Update(model, email);

        return updateResult is not null ? Ok(updateResult.Email) : BadRequest();


    }

    [HttpGet("resendEmail/{email}")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Super Admin,Seller")]
    public async Task<ActionResult> ResendEmail(string email)
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



        if (email != emailToken)
        {
            var userRequest = await _userManager.FindByEmailAsync(emailToken);
            var roles = await _userManager.GetRolesAsync(userRequest);
            var isNotSuperAdmin = roles.FirstOrDefault("Super Admin") is null;
            if (isNotSuperAdmin)
            {
                return Unauthorized();
            }

        }

        var userDb = await _userManager.FindByEmailAsync(email);

        if (userDb is null)
        {
            return BadRequest();
        }

        if(userDb.EmailConfirmed){
            return BadRequest();
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(userDb);

        var uriBuilder = new UriBuilder(_config["ReturnPaths:ConfirmEmail"]!);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);
        query["token"] = token;
        query["userid"] = userDb!.Id;
        uriBuilder.Query = query.ToString();
        var urlString = uriBuilder.ToString();

        var message = @"<p>Oi tudo bem? Estamos aqui para confirmar o seu cadastro no site da <b>ChikPet</b>. Seu cadastro em para o email " + userDb.Email + ". Clique no link abaixo para confirmar o seu cadastro</p><br/><a href='" + urlString + "' style='display: block; padding:40px; text-align: center; font-size: 28px; font-weigh: 700; text-decoration: none; color: black; background: #d1d1d1; border-radius: 10px; margin-left: auto; margin-right: auto;'>Clique aqui para confimar</a> ";

        var senderEmail = _config["ReturnPaths:SenderEmail"];
        await _emailSender.SendEmailAsync(senderEmail!, userDb.Email!, "Confirme o seu e-mail", message, "Confirme o seu email");

        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("confirmEmail")]
    public async Task<ActionResult> ConfirmEmailAdmin(ConfirmEmailDTO confirmEmail)
    {
        var user = await _userManager.FindByIdAsync(confirmEmail.UserId);

        if (user is null)
        {
            return BadRequest("Usuário não foi encontrado");
        }

        var result = await _userManager.ConfirmEmailAsync(user, confirmEmail.Token);

        if (result.Succeeded)
        {
            return Ok();
        }

        return BadRequest("Não foi possível confirmar o email");
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
    public async Task<ActionResult> LoginUser([FromBody] LoginUserDTO model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email!);

        if (user is not null)
        {
            var result = await _signInManager.PasswordSignInAsync(user,
            model.Password!, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {

                IList<string> userRoles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model.Email));

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
        _mapper.Map<UpdateUserDTO, UserModel>(model, userToChange);

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
