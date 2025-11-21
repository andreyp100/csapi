namespace csapi.ErrorExceptions
{
  public abstract class ErrorException : Exception
  {
    public int StatusCode { get; }

    protected ErrorException(string message, int statusCode) : base(message)
    {
      StatusCode = statusCode;
    }
  }

  public class AlreadyExistsError : ErrorException
  {
    public AlreadyExistsError(string message) : base(message, StatusCodes.Status409Conflict) { }
  }

  public class ErrorResponse
  {
    public int Status { get; set; }
    public string Message { get; set; }

    public ErrorResponse(ErrorException error)
    {
      Status = error.StatusCode;
      Message = error.Message;
    }
  }
}
