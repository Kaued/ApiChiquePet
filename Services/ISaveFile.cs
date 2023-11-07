using NuGet.Protocol.Core.Types;

namespace APICatalogo.Services{

  public interface ISaveFile{

    public Task<string> SaveImage(IFormFile file);

    public void RemoveFile (string path);
  }
}