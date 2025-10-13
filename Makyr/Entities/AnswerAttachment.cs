using System;

namespace API.Entities;

public class AnswerAttachment
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public string AnswerId { get; set; } = string.Empty;
    public Answer Answer { get; set; } = null!;
}
