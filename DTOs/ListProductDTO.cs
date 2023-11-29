using System.ComponentModel.DataAnnotations;
using ApiChikPet.Models;

namespace ApiChikPet.DTOs
{

  public class ListProductDTO
  {

    public int ProductId { get; set; }  
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [Range(0, Double.PositiveInfinity)]
    public decimal Price { get; set;}
    [Required]
    public decimal Height {get; set; }
    [Required]
    public decimal Width {get; set; }
    [Required]
    public decimal Stock {get;set;}
    [Required]
    public int CategoryId {get; set;}
    public CategoryProd? category {get;set;}
    public ICollection<ImageUrl>? ImageUrl {get; set;}
  }
}