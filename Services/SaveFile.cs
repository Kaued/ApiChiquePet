
using System.Security.Cryptography;
using System.Text;

namespace APICatalogo.Services
{

  public class SaveFile : ISaveFile
  {
    public async Task<string> SaveImage(IFormFile file, IWebHostEnvironment _environment)
    {
      var extension = Path.GetExtension(file.FileName);

      if (extension is not null && extension!.Contains("image"))
      {
        throw new Exception("Extension invalid");
      }

      DateTime date = DateTime.Today;
      var fileName = date.ToFileTime() + Path.GetFileName(file.FileName);
      var path = Path.Combine(_environment.WebRootPath, "images", fileName.Trim());

      using (var stream = new FileStream(path, FileMode.Create))
      {
        await file.CopyToAsync(stream);
      }

      var result = "images/" + fileName;
      return result;
    }

    public void RemoveFile(string path, IWebHostEnvironment _environment)
    {
      var filePath = _environment.WebRootPath+"/"+path;
      FileInfo file = new FileInfo(filePath);

      if(!file.Exists){
        throw new Exception("Arquivo não existe");
      }else{
      file.Delete();
      }
    }
  }
}