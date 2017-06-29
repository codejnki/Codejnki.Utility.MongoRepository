using Codejnki.Utility.MongoRepository.Interfaces;

namespace Codejnki.Utility.MongoRepository.Models
{
  public class Result : IResult
  {
    public bool Success { get; set; }
    public string Message { get; set; }
    public int ErrorCode { get; set; }

    public Result()
    {
      Success = false;
      Message = string.Empty;
      ErrorCode = 500;
    }
  }
}
