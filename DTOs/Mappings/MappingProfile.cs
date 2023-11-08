using ApiCatalogo.DTOs.Users;
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
      CreateMap<Category, CategoryDTO>().ReverseMap();
      CreateMap<Category, ListCategoryDTO>().ReverseMap();

      // Admin
      CreateMap<UserModel, RegisterUserDTO>().ReverseMap();
      CreateMap<UserModel, ListUserDTO>().ReverseMap();
      CreateMap<UserModel, UpdateUserDTO>().ReverseMap();
      CreateMap<UserModel, LoginUserDTO>().ReverseMap();

      // Pagination
      CreateMap(typeof(PageList<>), typeof(PaginationDTO)).ReverseMap();
    }
  }
}
