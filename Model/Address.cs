using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ApiChikPet.Models;


public class Address
{
  [Key]
  public int AddressId { get; set; }

  [Required]
  [StringLength(8)]
  public string? Cep { get; set; }

  [Required]
  [StringLength(128)]
  public string? Street { get; set; }

  [Required]
  [StringLength(128)]
  public string? Neighborhood { get; set; }

  [Required]
  [StringLength(128)]
  public string? City { get; set; }

  [Required]
  [StringLength(128)]
  public string? District { get; set; }

  [Required]
  public int Number { get; set; }

  [StringLength(64)]
  public string? Complement { get; set; }

  public string? UserId { get; set; }

  public UserModel? User { get; set; }

}