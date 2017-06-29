namespace Codejnki.Utility.MongoRepository.Interfaces

{
  public interface IGetSingleResult<TEntity> where TEntity : class, new()
  {
    TEntity Entity { get; set; }
  }
}