using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Codejnki.Utility.MongoRepository.Interfaces
{
  public interface IMongoDbRepository
  {
    Task<IGetSingleResult<TEntity>> GetOneAsync<TEntity>(Guid id) where TEntity : class, new();

    Task<IGetSingleResult<TEntity>> GetOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new();

    Task<IGetManyResult<TEntity>> GetAllAsync<TEntity>() where TEntity : class, new();

    Task<IGetSingleResult<TEntity>> AddOneAsync<TEntity>(TEntity item) where TEntity : class, new();

    Task<IResult> UpdateOneAsync<TEntity>(Guid id, TEntity update) where TEntity : class, new();

    Task<IResult> DeleteOneAsync<TEntity>(Guid id) where TEntity : class, new();

    Task<bool> ExistsAsync<TEntity>(Guid id) where TEntity : class, new();
  }
}
