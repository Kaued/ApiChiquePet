using ApiChikPet.DTOs.Users;
using ApiChikPet.Pagination;
using ApiChikPet.Models;
using AutoMapper;

namespace ApiChikPet.DTOs.Mappings
{

  public class MappingProfile : Profile
  {

    public MappingProfile()
    {
      // Category
      CreateMap<Category, CategoryDTO>().ReverseMap();
      CreateMap<Category, ListCategoryDTO>().ReverseMap();
      
      // Product
      CreateMap<Product, ProductDTO>().ReverseMap();
      CreateMap<Product, ListProductDTO>().ReverseMap();
      CreateMap<Category, CategoryProd>().ReverseMap();

      // ImagesProduct
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

      // Order
      CreateMap<Order, ListOrderDTO>().ReverseMap();
      CreateMap<OrderProduct, OrderProductDTO>().ReverseMap();

      // Pagination
      CreateMap(typeof(PageList<>), typeof(PaginationDTO)).ReverseMap();
    }
  }
}
