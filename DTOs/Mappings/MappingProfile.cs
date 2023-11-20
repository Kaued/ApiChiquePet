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
      CreateMap<Product, ProductDTO>().ReverseMap();
      CreateMap<Category, CategoryDTO>().ReverseMap();
      CreateMap<Category, ListCategoryDTO>().ReverseMap();

      CreateMap<Product, ListProductDTO>().ReverseMap();
      CreateMap<Category, CategoryProd>().ReverseMap();

      CreateMap<ImageUrl, ImageUrlDTO>().ReverseMap();
      CreateMap<ImageUrl, ListImageUrlDTO>().ReverseMap();

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
