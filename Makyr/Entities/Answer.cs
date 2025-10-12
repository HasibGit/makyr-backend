using System;

namespace API.Entities;

public class Answer
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? Attachments { get; set; }
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
