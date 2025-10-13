using System;

namespace API.Entities;

public class Question
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string? Tags { get; set; }
    public List<QuestionAttachment> Attachments { get; set; } = new();
    public int Upvotes { get; set; }
    public int Downvotes { get; set; }
    public bool IsResolved { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    public string AppUserId { get; set; } = string.Empty;
    public AppUser AppUser { get; set; } = null!;

    public ICollection<Answer> Answers { get; set; } = new List<Answer>();
}
