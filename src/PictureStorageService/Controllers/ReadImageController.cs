using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace PictureStorageService.Controllers;

[Route("img")]
[ApiController]
public class ReadImageController : ControllerBase
{
    [HttpGet("{img-id}")]
    public IActionResult GetImgById([FromRoute(Name ="img-id")] string id)
    {

        string path = 
            $""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
             /img-storage/{id}
             """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""";

        FileStream fsSource = new(path, FileMode.Open, FileAccess.Read);

        return File(fsSource, Image.Jpeg);
    }
}
