using System;

namespace API.Entities;

public class AnswerAttachment
{
    public int Id { get; set; }
    public string Url { get; set; } = string.Empty;

    public int AnswerId { get; set; }
    public Answer Answer { get; set; } = null!;
}
