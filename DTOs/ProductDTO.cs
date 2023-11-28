using System.ComponentModel.DataAnnotations;
using ApiChikPet.Models;

namespace ApiChikPet.DTOs
{

  public class ProductDTO
  {

    public int? ProductId { get; set; }  
    [Required]
    [StringLength(60)]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public string PriceStr { get; set; }
    public IFormFile Cover {get; set;}
    public ICollection<IFormFile> Gallery { get; set; }
    [Required]
    public string HeightStr {get; set; }
    [Required]
    public string WidthStr {get; set; }
    [Required]
    [Range(0, int.MaxValue)]
    public float Stock {get; set;}
    [Required]
    public int CategoryId {get; set;}

  }
}