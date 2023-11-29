using System.ComponentModel.DataAnnotations;

namespace ApiChikPet.DTOs{

  public class ChangeOrderDTO{

    [Required]
    public int StatusId {get;set;}
  }
}