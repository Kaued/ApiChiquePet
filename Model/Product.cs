using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Product")]
public class Product
{
  // public Produto(){
  //   this.imagemUrl = new Collection<ImagemUrl>();
  // }
  [Key]
  public int ProductId {get; set;}
  [Required]
  [StringLength(300)]
  public string? Name {get; set;}
  [Required]
  [Column(TypeName = "text")]
  public string? Description {get; set;}
  [Required]
  [Column(TypeName = "decimal(7, 2)")]
  public decimal Price {get; set;}
  [Required]
  [Column(TypeName = "decimal(6, 2)")]
  public decimal Height {get; set;}
  [Required]
  [Column(TypeName = "decimal(6, 2)")]
  public decimal Width {get; set;}
  [Required]
  public float Stock {get; set;}
  public DateTime? DateRegister{get; set;}  
  public int CategoryId {get; set;}
  [JsonIgnore]
  public Category? Category {get; set;}
  [JsonIgnore]
  public ICollection<ImageUrl>? imageUrl {get; set;}
}