namespace NoteLiveBackend.Rooms.Domain.Model.Aggregates;

public partial class Presentation
{
    public int Id { get; }
    public string Title { get; private set; }
    public string Description { get; private set; }

    public Presentation(string title, string description)
    {
        Title = title;
        Description = description;
    }
}