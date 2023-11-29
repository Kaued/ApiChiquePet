namespace ApiChikPet.Services
{

    public interface ISaveFile{

    public Task<string> SaveImage(IFormFile file);

    public void RemoveFile (string path);
  }
}