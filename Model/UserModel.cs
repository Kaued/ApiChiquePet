using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Models;


public class UserModel : IdentityUser
{  
  public UserModel(){
    Address = new Collection<Address>();
  }
  [Required]
  public DateTime BirthDate {get; set;}

  public ICollection<Address> Address {get; set;}
}