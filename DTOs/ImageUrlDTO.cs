using System.ComponentModel.DataAnnotations;
using ApiChikPet.Models;

namespace ApiChikPet.DTOs
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