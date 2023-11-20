using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APICatalogo.Models;

public class CategoryProd
{ 
  
  [Key]
  public int CategoryId {get; set;}
  [Required]
  [StringLength(300)]
  public string? Name {get; set;}
  [Required]
  [StringLength(300)]
  public string? ImageUrl{get; set;}
  
}