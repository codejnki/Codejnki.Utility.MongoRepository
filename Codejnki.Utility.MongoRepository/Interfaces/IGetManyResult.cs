using System.Collections.Generic;

namespace Codejnki.Utility.MongoRepository.Interfaces
{
  public interface IGetManyResult<TEntity> where TEntity : class, new()
  {
    IEnumerable<TEntity> Entities { get; set; }
  }
}