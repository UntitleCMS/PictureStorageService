using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using static System.Net.Mime.MediaTypeNames;

namespace PictureStorageService.Controllers;

[Route("img")]
[ApiController]
public class ReadImageController : ControllerBase
{

    private readonly IContentTypeProvider _contentTypeProvider;

    public ReadImageController(IContentTypeProvider contentTypeProvider)
    {
        _contentTypeProvider = contentTypeProvider;
    }

    [HttpGet("{img-name}")]
    public async Task<IActionResult> GetImgById([FromRoute(Name ="img-name")] string img_name)
    {

        string path = 
            $""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
             /img-storage/{img_name}
             """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""";

        FileStream fsSource = new(path, FileMode.Open, FileAccess.Read);

        _contentTypeProvider.TryGetContentType(path, out string? contentType);

        return contentType is null
            ? NotFound()
            : File(fsSource, contentType);
    }
}
