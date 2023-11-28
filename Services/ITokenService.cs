using ApiChikPet.DTOs;
using ApiChikPet.Models;

namespace ApiChikPet.Service;

public interface ITokenService{
  TokenDTO GerarToken(string key, string issues, string audience, UserModel user, IList<string> userRoles);
}