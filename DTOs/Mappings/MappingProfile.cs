using ApiCatalogo.DTOs.Admin;
using ApiCatalogo.Pagination;
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

      // Admin
      CreateMap<UserModel, RegisterDTO>().ReverseMap();
      CreateMap<UserModel, ListAdminDTO>().ReverseMap();
      CreateMap<UserModel, UpdateAdminDTO>().ReverseMap();
      CreateMap<UserModel, LoginDTO>().ReverseMap();

      // Pagination
      CreateMap(typeof(PageList<>), typeof(PaginationDTO)).ReverseMap();
    }
  }
}
