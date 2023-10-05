using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Models;


public class AddressModel : IdentityUser
{   
 [Key] 
  public int AdressId {get;set;}

  [Required]
  [StringLength(8)]
  public string? Cep {get;set;}

  [Required]
  [StringLength(128)]
  public string? Street {get;set;}

  [Required]
  [StringLength(128)]
  public string? Neighborhood {get;set;}

  [Required]
  [StringLength(128)]
  public string? City {get;set;}

  [Required]
  [StringLength(128)]
  public string? District {get;set;}

  [Required]
  public int Number {get;set;}

  [StringLength(64)]
  public string? Complent {get;set;}

  public int UserId{get;set;}

  public UserModel? User{get; set;}

}