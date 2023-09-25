using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class LoginController : ControllerBase
{
    private readonly AppDbContext _context;
    private IConfiguration Configuration;

    private readonly ITokeService tokeService;

    public LoginController(AppDbContext context, IConfiguration configuration, ITokeService tokenServices)
    {
        Configuration=(IConfigurationRoot)configuration;
        _context = context;
        tokeService=tokenServices;
    }

    [HttpPost]
    [AllowAnonymous]
    public ActionResult Post(UserModel user){
        if (user == null){
            return BadRequest("Login invalido");
        }

        if (user.Email == "teste" && user.Password == "teste"){
            var tokenString = tokeService.GerarToken(Configuration["Jwt:Key"],
                                                     Configuration["Jwt:Issuer"],
                                                     Configuration["Jwt:Audience"],
                                                     user);

            return Ok(new {tokern = tokenString});
        }
        else{
            return BadRequest("Login Invalido");
        }
    }
}
