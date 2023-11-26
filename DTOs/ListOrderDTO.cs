using ApiCatalogo.DTOs.Users;
using APICatalogo.Models;

namespace ApiCatalogo.DTOs{

  public class ListOrderDTO{

    public int OrderId {get; set;}
    public double TotalPrice { get; set; }
    public DateTime CreateDate { get; set; }
    public int StatusOrder { get; set; }
    public int AddressId { get; set; }
    public string UserId { get; set; }
    public ListUserDTO? User { get; set; }
    public ListAddressDTO? Address { get; set; }
    public ICollection<OrderProductDTO> OrderProducts { get; set; }
  }
}