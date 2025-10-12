using System;
using Microsoft.AspNetCore.Identity;

namespace API.Entities;

public class AppUser : IdentityUser
{
    public DateTime DateOfBirth { get; set; }
    public string KnownAs { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public string? Gender { get; set; }
    public string? About { get; set; }
    public string? Interests { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    // Navigation Properties
    public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    public ICollection<Question> Questions { get; set; } = new List<Question>();
    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
