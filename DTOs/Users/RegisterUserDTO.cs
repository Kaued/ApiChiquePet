using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs.Users
{

  public class RegisterUserDTO
  { 
    [Required]
    public string UserName{get; set;}
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string PhoneNumber {get; set;}
    [Required]
    public DateTime BirthDate {get; set;}
    
  }
}