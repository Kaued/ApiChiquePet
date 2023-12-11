using System.ComponentModel.DataAnnotations;

namespace ApiChikPet.DTOs.Users
{

  public class RegisterUserDTO
  {
    public string? UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string FullName { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    [Required]
    public DateTime BirthDate { get; set; }

  }
}