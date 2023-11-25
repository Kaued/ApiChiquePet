using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs{

  public class ChangeOrderDTO{

    [Required]
    public int StatusId {get;set;}
  }
}