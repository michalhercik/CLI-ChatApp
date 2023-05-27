public record Message
{
    public string? From { get; set; }
    public string? Text { get; set; }

    public Message(string? from, string text)
    {
        From = from;
        Text = text;
    }

    public Message(string text)
    {
        Text = text;
    }

    public Message() { }
}
