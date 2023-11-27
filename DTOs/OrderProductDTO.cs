using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiCatalogo.DTOs;

namespace APICatalogo.Models
{

  public class OrderProductDTO
  {
    public int Qtd { get; set; }
    public ListProductDTO? Product { get; set; }
    
  }
}