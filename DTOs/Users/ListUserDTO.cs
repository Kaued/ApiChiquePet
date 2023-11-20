using System.ComponentModel.DataAnnotations;
using APICatalogo.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiCatalogo.DTOs.Users
{

  public class ListUserDTO
  {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }

  }
}