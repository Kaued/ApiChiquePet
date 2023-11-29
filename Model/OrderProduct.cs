using System.ComponentModel.DataAnnotations;

namespace ApiChikPet.Models
{

  public class OrderProduct
  {

    [Key]
    public int OrderProductId { get; set; }
    [Required]
    public int ProductId { get; set; }
    [Required]
    public int OrderId { get; set; }
    [Required]
    public int Qtd { get; set; }
    public Product? Product { get; set; }
    public Order? Order { get; set; }
  }
}