using System;

namespace API.Entities;

public class QuestionAttachment
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public int QuestionId { get; set; }
    public Question Question { get; set; } = null!;
}
