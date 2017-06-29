using MongoDB.Driver;

namespace Codejnki.Utility.MongoRepository.Interfaces
{
  public interface IMongoDbContext
  {
    IMongoCollection<TEntity> GetCollection<TEntity>();
  }
}
