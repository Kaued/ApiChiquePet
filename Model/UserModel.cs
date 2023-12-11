using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ApiChikPet.Models;


public class UserModel : IdentityUser
{  
  public UserModel(){
    Address = new Collection<Address>();
  }
  [Required]
  public DateTime BirthDate {get; set;}
  
  [Required]
  [StringLength(123)]
  public string FullName {get; set;}

  public ICollection<Address> Address {get; set;}
}