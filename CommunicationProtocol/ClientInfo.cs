public record ClientInfo
{
    public string? Name { get; set; }
    public string? Thread { get; set; }

    public ClientInfo() { }

    public ClientInfo(string name, string? thread)
    {
        Name = name;
        Thread = thread;
    }
}
