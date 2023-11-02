using NuGet.Protocol.Core.Types;

namespace APICatalogo.Services{

  public interface ISaveFile{

    public Task<string> SaveImage(IFormFile file, IWebHostEnvironment _environment);

    public void RemoveFile (string path, IWebHostEnvironment _environment);
  }
}