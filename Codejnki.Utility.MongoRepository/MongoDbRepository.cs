using Codejnki.Utility.MongoRepository.Helpers;
using Codejnki.Utility.MongoRepository.Interfaces;
using Codejnki.Utility.MongoRepository.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace Codejnki.Utility.MongoRepository
{
  /// <summary>
  /// The MongoRepository
  /// </summary>
  /// <seealso cref="Codejnki.Utility.MongoRepository.Interfaces.IMongoDbRepository" />
  public class MongoDbRepository : IMongoDbRepository
  {
    private readonly IMongoDbContext _mongoDbContext;

    /// <summary>
    /// Initializes a new instance of the <see cref="MongoDbRepository"/> class.
    /// </summary>
    /// <param name="mongoDbContext">The mongo database context.</param>
    public MongoDbRepository(IMongoDbContext mongoDbContext)
    {
      _mongoDbContext = mongoDbContext;
    }

    /// <summary>
    /// Gets the collection.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns></returns>
    private IMongoCollection<TEntity> GetCollection<TEntity>() where TEntity : class, new()
    {
      return _mongoDbContext.GetCollection<TEntity>();
    }

    /// <summary>
    /// Asyncrounously determines if it exists
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns><c>true</c> if record exits</returns>
    public async Task<bool> ExistsAsync<TEntity>(Guid id) where TEntity : class, new()
    {
      var collection = GetCollection<TEntity>();
      var query = new BsonDocument("_id", id);
      var cursor = collection.Find(query);
      var count = await cursor.CountAsync();
      return (count > 0);
    }

    /// <summary>
    /// Gets all asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns></returns>
    public async Task<IGetManyResult<TEntity>> GetAllAsync<TEntity>() where TEntity : class, new()
    {
      var result = new GetManyResult<TEntity>();

      try
      {
        var collection = GetCollection<TEntity>();
        var entities = await collection.Find(new BsonDocument()).ToListAsync();
        if (entities == null == false)
        {
          result.Entities = entities;
        }

        result.Success = true;
        return result;
      }
      catch (Exception ex)
      {
        result.Message = NotificationHelper.NotifyException("GetAllAsync", "Exception getting all " + typeof(TEntity).Name + "s", ex);
        return result;
      }
    }

    /// <summary>
    /// Gets the one asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public async Task<IGetSingleResult<TEntity>> GetOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
    {
      var response = new GetSingleResult<TEntity>();
      try
      {
        var collection = GetCollection<TEntity>();
        var entity = await collection.Find(filter).SingleOrDefaultAsync();

        if (entity == null == false)
        {
          response.Entity = entity;
        }

        response.Success = true;
        return response;
      }
      catch (Exception ex)
      {
        response.Message = NotificationHelper.NotifyException("GetOne", "Exception getting one " + typeof(TEntity).Name, ex);
        return response;
      }

    }

    /// <summary>
    /// Adds the one asyncrounously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="item">The item.</param>
    /// <returns></returns>
    public async Task<IGetSingleResult<TEntity>> AddOneAsync<TEntity>(TEntity item) where TEntity : class, new()
    {
      var res = new GetSingleResult<TEntity>();
      try
      {
        var collection = GetCollection<TEntity>();
        await collection.InsertOneAsync(item);
        res.Success = true;
        res.Message = "OK";
        return res;
      }
      catch (Exception ex)
      {
        res.Message = NotificationHelper.NotifyException("AddOne", "Exception adding one " + typeof(TEntity).Name, ex);
        return res;
      }
    }

    /// <summary>
    /// Updates the one asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier.</param>
    /// <param name="update">The update.</param>
    /// <returns></returns>
    public async Task<IResult> UpdateOneAsync<TEntity>(Guid id, TEntity update) where TEntity : class, new()
    {
      var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
      return await UpdateOneAsync<TEntity>(filter, update);
    }

    /// <summary>
    /// Updates the one asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="filter">The filter.</param>
    /// <param name="update">The update.</param>
    /// <returns></returns>
    private async Task<IResult> UpdateOneAsync<TEntity>(FilterDefinition<TEntity> filter, TEntity update) where TEntity : class, new()
    {
      var result = new Result();
      try
      {
        var colleciton = GetCollection<TEntity>();
        var updateResponse = await colleciton.ReplaceOneAsync(filter, update);
        if (updateResponse.ModifiedCount < 1)
        {
          var ex = new Exception();
          result.Message = NotificationHelper.NotifyException("UpdateOne", "ERROR: updateRes.ModifiedCount < 1 for entity: " + typeof(TEntity).Name, ex);
          return result;
        }
        result.Success = true;
        result.Message = "OK";
        return result;
      }
      catch (Exception ex)
      {
        result.Message = NotificationHelper.NotifyException("UpdateOne", "Exception updating entity: " + typeof(TEntity).Name, ex);
        return result;
      }
    }

    /// <summary>
    /// Deletes the one asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<IResult> DeleteOneAsync<TEntity>(Guid id) where TEntity : class, new()
    {
      var filter = new FilterDefinitionBuilder<TEntity>().Eq("Id", id);
      return await DeleteOneAsync<TEntity>(filter);
    }

    /// <summary>
    /// Deletes the one asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="filter">The filter.</param>
    /// <returns></returns>
    public async Task<IResult> DeleteOneAsync<TEntity>(FilterDefinition<TEntity> filter) where TEntity : class, new()
    {
      var result = new Result();
      try
      {
        var collection = GetCollection<TEntity>();
        var deleteResult = await collection.DeleteOneAsync(filter);
        result.Success = true;
        result.Message = "OK";
        return result;
      }
      catch (Exception ex)
      {
        result.Message = NotificationHelper.NotifyException("DeleteOne", "Exception deleting one " + typeof(TEntity).Name, ex);
        return result;
      }
    }

    /// <summary>
    /// Gets the one asynchronously.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public async Task<IGetSingleResult<TEntity>> GetOneAsync<TEntity>(Guid id) where TEntity : class, new()
    {
      var filter = Builders<TEntity>.Filter.Eq("_id", id);
      return await GetOneAsync<TEntity>(filter);
    }
  }
}
