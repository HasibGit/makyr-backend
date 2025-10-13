using System;

namespace API.Entities;

public class QuestionAttachment
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public string QuestionId { get; set; } = string.Empty;
    public Question Question { get; set; } = null!;
}
