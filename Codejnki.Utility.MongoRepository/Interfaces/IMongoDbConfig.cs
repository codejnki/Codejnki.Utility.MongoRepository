namespace Codejnki.Utility.MongoRepository.Interfaces
{
  public interface IMongoDbConfig
  {
    string ConnectionString { get; set; }

    string DatabaseName { get; set; }
  }
}
