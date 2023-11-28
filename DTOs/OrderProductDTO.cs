using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ApiChikPet.DTOs;

namespace ApiChikPet.Models
{

  public class OrderProductDTO
  {
    public int Qtd { get; set; }
    public ListProductDTO? Product { get; set; }
    
  }
}