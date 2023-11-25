

using System.ComponentModel.DataAnnotations;
using ApiCatalogo.Class;

namespace ApiCatalogo.DTOs{

  public class RegisterOrder{

    [Required]
    public List<ItemOrder> Item {get; set;} = new List<ItemOrder>();
    [Required]
    public int AddressId {get; set;}
    [Required]
    public bool IsOrder {get; set;}
    
  }
}