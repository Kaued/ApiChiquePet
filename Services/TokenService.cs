using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ApiChikPet.DTOs;
using ApiChikPet.Models;
using ApiChikPet.Service;
using Microsoft.IdentityModel.Tokens;

namespace ApiChikPet.Services;

public class TokenService: ITokenService {

  public TokenDTO GerarToken(string key, string issues, string audience, UserModel user, IList<string> userRoles){
    var claims = new List<Claim>(){
      new Claim(ClaimTypes.Name, user.Email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    foreach (var role in userRoles){
      claims.Add(new Claim(ClaimTypes.Role, role));
    }

    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
    
    var token = new JwtSecurityToken(issuer: issues,
                                    audience: audience,
                                    claims: claims,
                                    expires: DateTime.Now.AddMinutes(120),
                                    signingCredentials: credentials);
    
    var tokenHandler = new JwtSecurityTokenHandler();
    var stringToken = tokenHandler.WriteToken(token);


    
    return new TokenDTO(){
      Authenticated=true,
      Token=stringToken,
      Expiration= DateTime.Now.AddMinutes(120),
      Roles = userRoles,
      Email = user.Email
    };
  }
}