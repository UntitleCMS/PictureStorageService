using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using PictureStorageService.Datas;

namespace PictureStorageService.Controllers;

[Route("avatar")]
[ApiController]
public class AvatarController : ControllerBase
{
    private readonly IContentTypeProvider _contentTypeProvider;
    private readonly AppDbContext _appDbContext;

    public AvatarController(IContentTypeProvider contentTypeProvider, AppDbContext appDbContext)
    {
        _contentTypeProvider = contentTypeProvider;
        _appDbContext = appDbContext;
    }

    [HttpGet("{uid}")]
    public IActionResult GetAvater([FromRoute(Name ="uid")]string id)
    {
        //var a = _appDbContext.Avatars.FirstOrDefault(a=>a.Id == id);

        //if (a is null)
        //    return NotFound();

        string path = 
            $""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""
             /img-storage/avatar-{id}.jpg
             """""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""""";

        try
        {

            FileStream fsSource = new(path, FileMode.Open, FileAccess.Read);

            _contentTypeProvider.TryGetContentType(path, out string? contentType);

            return contentType is null
                ? NotFound()
                : File(fsSource, contentType);

        }catch (Exception)
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAvatar(
        IFormFile avatar,
        string sub)
    {
        // reject if not images
        if (! avatar.ContentType.StartsWith("image/") )
            return BadRequest($"{avatar.FileName} is not image");

        //var fileName = $"avatar-{sub}.{avatar.FileName.Split(".").Last()}";
        var fileName = $"avatar-{sub}.jpg";

        using (Stream fileStream = new FileStream($"/img-storage/{fileName}", FileMode.Create))
        {
            avatar.CopyTo(fileStream);
        }

        var a = _appDbContext.Avatars.FirstOrDefault(a => a.Id == sub);

        if (a is null)
            _appDbContext.Avatars.Add(new()
            {
                Id = sub,
                ImageName = fileName,
                Size = avatar.Length
            }) ;
        else
        {
            a.Size = avatar.Length; 
            //a.ImageName = fileName;
        }

        await _appDbContext.SaveChangesAsync();

        return Ok();
    }

}
