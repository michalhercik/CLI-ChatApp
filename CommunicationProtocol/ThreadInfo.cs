public record ThreadInfo
{
    public string? Name { get; set; }
    public ushort MembersCount { get; set; }

    public ThreadInfo() { }

    public ThreadInfo(string name, ushort membersCount)
    {
        Name = name;
        MembersCount = membersCount;
    }
}
