using System.ComponentModel.DataAnnotations;
using APICatalogo.Models;

namespace ApiCatalogo.DTOs
{
    public class ListImageUrlDTO
    {
        public int? ImageId { get; set; }
        [Required]
        public List<IFormFile> Imagens { get; set; }
        [Required]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        public string Type {get; set;}
    }
}