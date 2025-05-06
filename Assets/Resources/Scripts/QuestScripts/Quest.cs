public class Quest
{
    public string Title { get; }
    public string Description { get; }
    public bool IsCompleted { get; private set; }

    public Quest(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public void Complete() => IsCompleted = true;
}
