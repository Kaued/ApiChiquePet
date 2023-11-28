using System.ComponentModel.DataAnnotations;
using ApiChikPet.Models;

namespace ApiChikPet.DTOs
{

  public class CategoryDTO
  {
    public int CategoryId { get; set; }
    [Required]
    public string? Name { get; set; }
    // [Required]
    public IFormFile File { get; set; }
    public ICollection<ListProductDTO>? Products {get; set;}
  }
}