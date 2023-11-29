using System.ComponentModel.DataAnnotations;

namespace ApiChikPet.DTOs.Users
{

  public class UpdateUserDTO
  { 
    [Required]
    public string UserName{get; set;}
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    public string? Password { get; set; }
    [Required]
    public string PhoneNumber {get; set;}
    [Required]
    public DateTime BirthDate {get; set;}
    
  }
}