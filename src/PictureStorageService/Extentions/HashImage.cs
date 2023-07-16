using System.Security.Cryptography;

namespace PictureStorageService.Extentions;

public static class HashImage
{
    public static string B64UrlHashName(this IFormFile img)
    {
        var fileHash = SHA256.Create().ComputeHash(img.OpenReadStream());
        string fileName = Convert.ToBase64String(fileHash)
            .TrimEnd('=')
            .Replace("/", "-")
            .Replace("+", "_");
        fileName += "." + img.FileName.Split(".").Last();
        return fileName; 
    }
}
