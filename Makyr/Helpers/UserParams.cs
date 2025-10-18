using System;

namespace API.Helpers;

public class UserParams : PaginationParams
{
    public string? UserName { get; set; }
}
