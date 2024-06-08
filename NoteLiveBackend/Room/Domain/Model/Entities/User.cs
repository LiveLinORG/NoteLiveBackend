namespace NoteLiveBackend.Room.Domain.Model.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public User(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
