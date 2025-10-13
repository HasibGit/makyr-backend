using System;

namespace API.Helpers;

public class ApiError
{
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
