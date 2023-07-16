using MongoFramework;
using PictureStorageService.Entitis;

namespace PictureStorageService.Datas;

public class AppDbContext : MongoDbContext
{
    public MongoDbSet<ImageModel> Images { get; set; }
    public MongoDbSet<AvatarModel> Avatars { get; set; }
    public AppDbContext(IMongoDbConnection connection) : base(connection) { }
}
