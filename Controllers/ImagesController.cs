using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace ApiCatalogo.Controllers
{
    [Route("download")]
  [EnableCors("Admin")]
  [ApiController]
  [AllowAnonymous]
  public class ImagesController : ControllerBase
  {

    private readonly IWebHostEnvironment _environment;

    public ImagesController(IWebHostEnvironment environment){
      _environment=environment;
    }

    [HttpGet("{path}/{fileName}")]
    public IActionResult GetImageFile(string path, string fileName)
    {

        try
        { 
            path = path + "/";
            string filePath = Path.Combine(_environment.WebRootPath, path, fileName);
            FileInfo file = new FileInfo(filePath);
        
            if (file.Exists)
            { 
                var stream = new FileStream(filePath, FileMode.Open);
                return File(stream, "application/octet-stream", fileName);
            }
            return NotFound(filePath);
        }
        catch (Exception ex)
        {
           return BadRequest(ex);
        }

    }
  }
}