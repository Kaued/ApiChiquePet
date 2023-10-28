
[Table("Produtos")]
public class ImagemUrl
{
    [Key]
    public int ImagemId {get; set;}
    [Required]
    [StringLength(300)]
    public string? Url {get; set;}
}