
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using ApiChikPet.Models;

[Table("ImageUrl")]
public class ImageUrl
{
    [Key]
    public int ImageId { get; set; }
    [Required]
    [StringLength(300)]
    public string? Path { get; set; }
    [Required]
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product? Product { get; set; }
    [Required]
    [StringLength(20)]
    public string Type {get; set;}
}