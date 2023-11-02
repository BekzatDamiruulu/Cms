namespace Cms.Shared.Shared.Models;

public class ErrorResponse
{
    public string Message { get; }
    public string? InnerMessage { get; }

    public ErrorResponse(Exception exception)
    {
        Message = exception.Message;
        InnerMessage = exception.InnerException?.Message;
    }
}