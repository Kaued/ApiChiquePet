using System.ComponentModel.DataAnnotations;
using APICatalogo.Models;

namespace ApiCatalogo.DTOs
{
    public class ImageUrlDTO
    {
        public int? ImageId { get; set; }
        [Required]
        public List<IFormFile> Imagens { get; set; }
        public int? ProductId { get; set; }
        [Required]
        public string Type {get; set;}
    }
}