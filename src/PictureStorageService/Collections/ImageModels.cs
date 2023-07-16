using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace PictureStorageService.Entitis;

[Table("Images")]
public class ImageModel
{
    public string? Id { get; set; }
    public HashSet<string> OwnersId { get; set; } = new HashSet<string>();
    public long Size { get; set; }
    //public string ImageName { get; set; } = string.Empty;
}
