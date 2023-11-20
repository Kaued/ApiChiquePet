using System.ComponentModel.DataAnnotations;
using APICatalogo.Models;

namespace ApiCatalogo.DTOs
{

  public class ProductDTO
  {

    public int? ProductId { get; set; }  
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    [Range(0, Double.PositiveInfinity)]
    public decimal Price { get; set; }
    public IFormFile Cover {get; set;}
    public ICollection<IFormFile> Gallery { get; set; }
    [Required]
    public decimal Height {get; set; }
    [Required]
    public decimal Width {get; set; }
    [Required]
    public float Stock {get; set;}
    [Required]
    public int CategoryId {get; set;}

  }
}