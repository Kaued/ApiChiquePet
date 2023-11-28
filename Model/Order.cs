using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiChikPet.Models{

  public class Order{

    public Order()
    {
      OrderProducts = new Collection<OrderProduct>();
    }
    
    [Key]
    public int OrderId {get; set;}
    [Required]
    [Column(TypeName = "decimal(7, 2)")]
    public decimal TotalPrice {get; set;}
    [Required]
    public DateTime CreateDate {get; set;}
    [Required]
    public int StatusOrder {get; set;}
    [Required]
    public bool IsOrder{get;set;}
    [Required]
    public int AddressId {get; set;}
    [Required]
    public string? UserId { get; set;}
    public UserModel? User { get; set; }
    public Address? Address { get; set; }
    public ICollection<OrderProduct> OrderProducts {get; set;}
  }
}