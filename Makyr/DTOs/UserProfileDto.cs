using System;

namespace API.DTOs;

public class UserProfileDto
{
    public DateTime DateOfBirth { get; set; }
    public string KnownAs { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string? Gender { get; set; }
    public string? About { get; set; }
    public string? Interests { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
    public string? PhotoUrl { get; set; }
}
