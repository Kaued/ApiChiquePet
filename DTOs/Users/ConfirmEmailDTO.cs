using System.ComponentModel.DataAnnotations;

namespace ApiChikPet.DTOs.Users{

  public class ConfirmEmailDTO{
    [Required]
    public string Token {get; set;}
    [Required]
    public string UserId {get; set;}
  }
}