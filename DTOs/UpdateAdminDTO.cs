using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs
{

  public class UpdateAdminDTO
  { 
    [Required]
    public string UserName{get; set;}
    [Required]
    public string Email { get; set; }
    public string Password { get; set; }
    [Required]
    public string PhoneNumber {get; set;}
    [Required]
    public DateTime BirthDate {get; set;}
    
  }
}