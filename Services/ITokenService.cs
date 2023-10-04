using ApiCatalogo.DTOs;
using APICatalogo.Models;

namespace APICatalogo.Service;

public interface ITokenService{
  TokenDTO GerarToken(string key, string issues, string audience, UserModel user);
}