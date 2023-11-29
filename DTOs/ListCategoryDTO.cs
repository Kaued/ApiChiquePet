using System.ComponentModel.DataAnnotations;
using ApiChikPet.Models;

namespace ApiChikPet.DTOs
{

  public class ListCategoryDTO
  {
    public int CategoryId { get; set; }
    [Required]
    public string? Name { get; set; }
    // [Required]
    public string ImageUrl { get; set; }
    public ICollection<ListProductDTO>? Products {get; set;}
  }
}