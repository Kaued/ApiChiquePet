using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiChikPet.Models;

public class Category
{ 
  
  public Category(){
    Products = new Collection<Product>();
    
  }
  [Key]
  public int CategoryId {get; set;}
  [Required]
  [StringLength(300)]
  public string? Name {get; set;}
  [Required]
  [StringLength(300)]
  public string? ImageUrl{get; set;}

  public ICollection<Product>? Products {get; set;}
  
}