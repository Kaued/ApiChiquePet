using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APICatalogo.Models
{

  public class OrderProductDTO
  {
    public int Qtd { get; set; }
    public Product? Product { get; set; }
    
  }
}