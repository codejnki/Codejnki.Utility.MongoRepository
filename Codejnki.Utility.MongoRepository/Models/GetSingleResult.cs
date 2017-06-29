using Codejnki.Utility.MongoRepository.Interfaces;

namespace Codejnki.Utility.MongoRepository.Models
{
  public class GetSingleResult<TEntity> : Result, IGetSingleResult<TEntity> where TEntity : class, new()
  {
    public TEntity Entity { get; set; }
  }
}
