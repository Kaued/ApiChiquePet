using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs.Admin
{

  public class LoginDTO
  { 
    [Required]  
    public string? Email { get; set; }
    [Required]
    public string? Password { get; set; }
    
  }
}