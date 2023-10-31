using APICatalogo.Models;

namespace ApiCatalogo.DTOs
{

  public class CategoryDTO
  {
    public int CategoryId { get; set; }
    public string? Nome { get; set; }
    public string? ImagemUrl { get; set; }
    public ICollection<ProdutoDTO>? Produtos {get; set;}
  }
}