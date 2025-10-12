using System;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;

}
