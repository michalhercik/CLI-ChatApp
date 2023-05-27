public record SetupRequest
{
    public string? Nick { get; set; }
    public string? Password { get; set; }

    public SetupRequest() { }

    public SetupRequest(string nick, string? password)
    {
        Nick = nick;
        Password = password;
    }
}
