using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs.Users
{

  public class LoginUserDTO
  { 
    [Required]  
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    
  }
}