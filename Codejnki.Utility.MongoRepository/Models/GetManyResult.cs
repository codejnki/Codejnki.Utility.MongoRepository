using Codejnki.Utility.MongoRepository.Interfaces;
using System.Collections.Generic;

namespace Codejnki.Utility.MongoRepository.Models
{
  public class GetManyResult<TEntity> : Result, IGetManyResult<TEntity> where TEntity : class, new()
  {
    public IEnumerable<TEntity> Entities { get; set; }
  }
}
