using Microsoft.AspNetCore.Identity;

namespace APICatalogo.Models;


public class UserModel : IdentityUser
{ 
  public string? Email{get; set;}

  public string? Password{get; set;} 

}