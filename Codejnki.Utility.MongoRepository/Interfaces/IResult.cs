namespace Codejnki.Utility.MongoRepository.Interfaces
{
  public interface IResult
  {
    int ErrorCode { get; set; }
    string Message { get; set; }
    bool Success { get; set; }
  }
}