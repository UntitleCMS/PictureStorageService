using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

namespace PictureStorageService.Entitis;

[Table("Avatar")]
public class AvatarModel
{
    public string? Id { get; set; }
    public long Size { get; set; }
    public string ImageName { get; set; } = string.Empty;
}
