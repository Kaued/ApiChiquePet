using APICatalogo.Models;
using AutoMapper;

namespace ApiCatalogo.DTOs.Mappings
{

  public class MappingProfile : Profile
  {

    public MappingProfile()
    {
      CreateMap<Produto, ProdutoDTO>().ReverseMap();
      CreateMap<Categoria, CategoriaDTO>().ReverseMap();
      CreateMap<UserModel, UserDTO>().ReverseMap();
      CreateMap<UserModel, LoginDTO>().ReverseMap();
    }
  }
}
