using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PictureStorageService.Datas;
using PictureStorageService.Extentions;
using System.Security.Cryptography;

namespace PictureStorageService.Controllers;

[Route("img")]
[ApiController]
public class SaveImageController : ControllerBase
{
    private readonly AppDbContext _appDbContext;

    public SaveImageController(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    [HttpPost]
    public IActionResult SaveImg( IFormFile img, [FromQuery]string sub)
    {
        // reject if not images
        if (! img.ContentType.StartsWith("image/") )
            return BadRequest($"{img.FileName} is not image");


        // save to server
        string fileName = img.B64UrlHashName();
        using (Stream fileStream = new FileStream($"/img-storage/{fileName}", FileMode.Create))
        {
            img.CopyTo(fileStream);
        }

        var imgMeta = _appDbContext.Images.FirstOrDefault(i=>i.Id == fileName);
        if (imgMeta is null)
        {
            _appDbContext.Images.Add(new()
            {
                Id = fileName,
                OwnersId = new() { sub },
                Size = img.Length
            });
        }
        else
        {
            imgMeta.OwnersId.Add(sub);
        }
        _appDbContext.SaveChanges();

        // return metadata of image
        return Ok(new
        {
            sub = sub,
            img = fileName
        });
    }
}
