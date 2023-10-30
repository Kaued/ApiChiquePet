
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using APICatalogo.Models;

[Table("Produtos")]
public class ImagemUrl
{
    [Key]
    public int ImagemId { get; set; }
    [Required]
    [StringLength(300)]
    public string? Url { get; set; }
    [Required]
    public int ProdutoId { get; set; }
    public Produto? Produto { get; set; }
}