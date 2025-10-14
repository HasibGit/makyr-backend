using System;

namespace API.Helpers;

public class ApiError
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;

    public ApiError(int statusCode, string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}
