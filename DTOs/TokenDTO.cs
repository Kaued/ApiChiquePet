namespace ApiCatalogo.DTOs
{

  public class TokenDTO
  {
    public bool Authenticated {get; set;}
    public DateTime Expiration {get; set;}
    public string Token {get; set;}
    public IList<string> Roles{get; set;}
    
  }
}