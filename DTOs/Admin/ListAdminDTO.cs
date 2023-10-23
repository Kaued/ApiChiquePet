using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.DTOs.Admin
{

  public class ListAdminDTO
  {
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }

  }
}