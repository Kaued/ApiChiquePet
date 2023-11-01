using System.ComponentModel.DataAnnotations;
using APICatalogo.Models;

namespace ApiCatalogo.DTOs
{

  public class ListCategoryDTO
  {
    public int CategoryId { get; set; }
    [Required]
    public string? Name { get; set; }
    // [Required]
    public string ImageUrl { get; set; }
    public ICollection<ProdutoDTO>? Produtos {get; set;}
  }
}