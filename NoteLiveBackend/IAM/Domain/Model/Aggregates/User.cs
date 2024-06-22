using System.Text.Json.Serialization;


public class User(string username, string passwordHash)
{
    public User() : this(String.Empty, String.Empty)
    {
        
    }
    
    public int Id { get; }
    public string Username { get; private set; } = username;
    [JsonIgnore] public string PasswordHash { get; private set; } = passwordHash;

    public User UpdateUsername(string username)
    {
        Username = username;
        return this;
    }
    
    public User UpdatePasswordHash(string passwordHash)
    {
        PasswordHash = passwordHash;
        return this;
    }
    
    
    
    
    
    
    
}