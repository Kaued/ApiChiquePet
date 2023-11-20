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
      CreateMap<Product, ProductDTO>().ReverseMap();
      CreateMap<Category, CategoryDTO>().ReverseMap();
      CreateMap<Category, ListCategoryDTO>().ReverseMap();

      CreateMap<Product, ListProductDTO>().ReverseMap();
      CreateMap<Category, CategoryProd>().ReverseMap();

      CreateMap<ImageUrl, ImageUrlDTO>().ReverseMap();
      CreateMap<ImageUrl, ListImageUrlDTO>().ReverseMap();

      // Admin
      CreateMap<UserModel, RegisterUserDTO>().ReverseMap();
      CreateMap<UserModel, ListUserDTO>().ReverseMap();
      CreateMap<UserModel, UpdateUserDTO>().ReverseMap();
      CreateMap<UserModel, LoginUserDTO>().ReverseMap();

      // Address
      CreateMap<Address, AddressDTO>().ReverseMap();
      CreateMap<Address, ListAddressDTO>().ReverseMap();

      // Pagination
      CreateMap(typeof(PageList<>), typeof(PaginationDTO)).ReverseMap();
    }
  }
}
