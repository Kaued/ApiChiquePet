using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace APICatalogo.Models;

[Table("Produtos")]
public class Produto
{
  // public Produto(){
  //   this.imagemUrl = new Collection<ImagemUrl>();
  // }
  [Key]
  public int ProdutoId {get; set;}
  [Required]
  [StringLength(300)]
  public string? Nome {get; set;}
  [Required]
  [StringLength(300)]
  public string? Descricao {get; set;}
  [Required]
  [Column(TypeName = "decimal(7, 2)")]
  public decimal Preco {get; set;}
  [Required]
  [Column(TypeName = "decimal(6, 2)")]
  public decimal Altura {get; set;}
  [Required]
  [Column(TypeName = "decimal(6, 2)")]
  public decimal Largura {get; set;}
  public float Estoque{get; set;}
  public DateTime? DataCadastro{get; set;}
  public int CategoriaId {get; set;}
  [JsonIgnore]
  public Categoria? Categoria {get; set;}
  // public ICollection<ImagemUrl>? imagemUrl {get; set;}
}