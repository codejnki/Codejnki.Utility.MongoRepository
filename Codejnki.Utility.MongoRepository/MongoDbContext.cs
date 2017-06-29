using Codejnki.Utility.MongoRepository.Interfaces;
using MongoDB.Driver;
using System.Security.Authentication;

namespace Codejnki.Utility.MongoRepository
{
  public class MongoDbContext : IMongoDbContext
  {
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDbContext"/> class.
    /// </summary>
    /// <param name="mongoDbConfig">The mongo database configuration.</param>
    public MongoDbContext(
      IMongoDbConfig mongoDbConfig
    )
    {

      MongoClientSettings settings = MongoClientSettings.FromUrl(
        new MongoUrl(mongoDbConfig.ConnectionString)
      );

      settings.SslSettings =
        new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

      _client = new MongoClient(settings);
      _database = _client.GetDatabase(mongoDbConfig.DatabaseName);
    }

    /// <summary>
    /// Gets the collection.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns></returns>
    public IMongoCollection<TEntity> GetCollection<TEntity>()
    {
      return _database.GetCollection<TEntity>(typeof(TEntity).Name.ToLower());
    }
  }
}
